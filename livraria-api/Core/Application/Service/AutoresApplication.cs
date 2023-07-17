using AutoMapper;
using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Service;

public class AutoresApplication : IAutoresApplication
{
    private IAutoresRepository _autoresRepository;
    private ILivrosRepository _livrosRepository;
    private IMapper _mapper;

    public AutoresApplication(
        IMapper mapper,
        IAutoresRepository autoresRepository,
        ILivrosRepository livrosRepository
    )
    {
        _mapper = mapper;
        _autoresRepository = autoresRepository;
        _livrosRepository = livrosRepository;
    }

    public async Task<Response<AutoresDto>> ObterPorIdAsync(int id)
    {
        Autores? autor = await _autoresRepository.ObterPorIdAsync(id);
        if (autor is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        AutoresDto autoresDto = _mapper.Map<Autores, AutoresDto>(autor);
        return new Response<AutoresDto>(autoresDto);
    }

    public async Task<Response<Paginator<AutoresDto>>> ObterTodosAsync(int pageSize, int pageNumber)
    {
        Paginator<Autores> autores = await _autoresRepository.ObterTodosAsync(pageSize, pageNumber);
        List<AutoresDto> autoresDto = _mapper.Map<List<Autores>, List<AutoresDto>>(autores.Itens);
        Paginator<AutoresDto> paginator = new Paginator<AutoresDto>(autores.PageSize, autores.PageNumber, autores.TotalItems, autoresDto);
        return new Response<Paginator<AutoresDto>>(paginator);
    }

    public async Task<Response<AutoresDto>> CadastrarAsync(CadastroAutorModel modelo)
    {
        Autores autor = new Autores(modelo);
        autor.IsValid();
        await _autoresRepository.AddAsync(autor);
        await _autoresRepository.CommitAsync();
        AutoresDto autoresDto = _mapper.Map<Autores, AutoresDto>(autor);
        return new Response<AutoresDto>(autoresDto);
    }

    public async Task<Response<AutoresDto>> AtualizarAsync(AtualizaAutorModel modelo)
    {
        Autores? autor = await _autoresRepository.ObterPorIdAsync(modelo.Id);
        if (autor is null)
        {
            throw new RegistroNaoLocalizadoException(modelo.Id.ToString());
        }
        autor.Atualizar(modelo);
        autor.IsValid();
        _autoresRepository.Update(autor);
        await _autoresRepository.CommitAsync();
        AutoresDto autoresDto = _mapper.Map<Autores, AutoresDto>(autor);
        return new Response<AutoresDto>(autoresDto);
    }

    public async Task<Response<AutoresDto>> DeletarAsync(int id)
    {
        Autores? autor = await _autoresRepository.ObterPorIdAsync(id);
        if (autor is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        if (await _livrosRepository.VerificarSeExisteAlgumLivroDoAutorAsync(id))
        {
            throw new DeletarAutorComLivroException(id.ToString());
        }
        _autoresRepository.Delete(autor);
        await _autoresRepository.CommitAsync();
        return new Response<AutoresDto>();
    }

}
