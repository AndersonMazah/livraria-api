using AutoMapper;
using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Service;

public class LivrosApplication : ILivrosApplication
{
    private ILivrosRepository _livrosRepository;
    private IVendasRepository _vendasRepository;
    private IMapper _mapper;

    public LivrosApplication(
        IMapper mapper,
        ILivrosRepository livrosRepository,
        IVendasRepository vendasRepository
    )
    {
        _mapper = mapper;
        _livrosRepository = livrosRepository;
        _vendasRepository = vendasRepository;
    }

    public async Task<Response<LivrosDetalheDto>> ObterPorIdAsync(int id)
    {
        Livros? livro = await _livrosRepository.ObterPorIdAsync(id);
        if (livro is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        LivrosDetalheDto livroDetalheDto = _mapper.Map<Livros, LivrosDetalheDto>(livro);
        return new Response<LivrosDetalheDto>(livroDetalheDto);
    }

    public async Task<Response<Paginator<LivrosDto>>> ObterTodosAsync(int pageSize, int pageNumber, int idAutor)
    {
        Paginator<Livros> livro = await _livrosRepository.ObterTodosAsync(pageSize, pageNumber, idAutor);
        List<LivrosDto> livrosDto = _mapper.Map<List<Livros>, List<LivrosDto>>(livro.Itens);
        Paginator<LivrosDto> paginator = new Paginator<LivrosDto>(livro.PageSize, livro.PageNumber, livro.TotalItems, livrosDto);
        return new Response<Paginator<LivrosDto>>(paginator);
    }

    public async Task<Response<LivrosDto>> CadastrarAsync(CadastroLivroModel modelo)
    {
        Livros livro = new Livros(modelo);
        livro.IsValid();
        await _livrosRepository.AddAsync(livro);
        await _livrosRepository.CommitAsync();
        LivrosDto livrosDto = _mapper.Map<Livros, LivrosDto>(livro);
        return new Response<LivrosDto>(livrosDto);
    }

    public async Task<Response<LivrosDto>> AtualizarAsync(AtualizaLivroModel modelo)
    {
        Livros? livro = await _livrosRepository.ObterPorIdAsync(modelo.Id);
        if (livro is null)
        {
            throw new RegistroNaoLocalizadoException(modelo.Id.ToString());
        }
        livro.Atualizar(modelo);
        livro.IsValid();
        _livrosRepository.Update(livro);
        await _livrosRepository.CommitAsync();
        LivrosDto livrosDto = _mapper.Map<Livros, LivrosDto>(livro);
        return new Response<LivrosDto>(livrosDto);
    }

    public async Task<Response<LivrosDto>> DeletarAsync(int id)
    {
        Livros? livro = await _livrosRepository.ObterPorIdAsync(id);
        if (livro is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        if (await _vendasRepository.VerificarSeExisteAlgumaVendaDoLivroAsync(id))
        {
            throw new DeletarLivroComVendaException(id.ToString());
        }
        _livrosRepository.Delete(livro);
        await _livrosRepository.CommitAsync();
        return new Response<LivrosDto>();
    }

    public async Task<Response<InfoLivroDto>> SalvarInfoLivroAsync(InfoLivroModel modelo)
    {
        Livros? livro = await _livrosRepository.ObterPorIdAsync(modelo.LivroId);
        if (livro is null)
        {
            throw new RegistroNaoLocalizadoException(modelo.LivroId.ToString());
        }
        livro.AtualizarInfo(modelo);
        livro.IsValid();
        _livrosRepository.Update(livro);
        await _livrosRepository.CommitAsync();
        InfoLivroDto infoLivroDto = _mapper.Map<Livros, InfoLivroDto>(livro);
        return new Response<InfoLivroDto>(infoLivroDto);
    }

    public async Task<Response<InfoLivroDto>> ObterInfoPorIdAsync(int id)
    {
        Livros? livro = await _livrosRepository.ObterPorIdAsync(id);
        if (livro is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        InfoLivroDto infoLivroDto = _mapper.Map<Livros, InfoLivroDto>(livro);
        return new Response<InfoLivroDto>(infoLivroDto);
    }

}
