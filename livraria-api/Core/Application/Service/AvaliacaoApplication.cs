using AutoMapper;
using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Service;

public class AvaliacaoApplication : IAvaliacaoApplication
{
    private IAvaliacaoRepository _avaliacaoRepository;
    private ILivrosRepository _livrosRepository;
    private IMapper _mapper;

    public AvaliacaoApplication(
        IMapper mapper,
        IAvaliacaoRepository avaliacaoRepository,
        ILivrosRepository livrosRepository
    )
    {
        _mapper = mapper;
        _avaliacaoRepository = avaliacaoRepository;
        _livrosRepository = livrosRepository;
    }

    public async Task<Response<AvaliacaoDto>> ObterPorIdAsync(int id)
    {
        Avaliacao? avaliacao = await _avaliacaoRepository.ObterPorIdAsync(id);
        if (avaliacao is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        AvaliacaoDto avaliacaoDto = _mapper.Map<Avaliacao, AvaliacaoDto>(avaliacao);
        return new Response<AvaliacaoDto>(avaliacaoDto);
    }

    public async Task<Response<List<AvaliacaoDto>>> ObterTodasAvaliacoesDoLivroAsync(int idLivro)
    {
        List<Avaliacao> avaliacao = await _avaliacaoRepository.ObterTodasAvaliacoesDoLivroAsync(idLivro);
        List<AvaliacaoDto> avaliacaoDto = _mapper.Map<List<Avaliacao>, List<AvaliacaoDto>>(avaliacao);
        return new Response<List<AvaliacaoDto>>(avaliacaoDto);
    }

    public async Task<Response<AvaliacaoDto>> CadastrarAsync(CadastroAvaliacaoModel modelo)
    {
        if (!await _avaliacaoRepository.VerificarSeLivroExisteAsync(modelo.LivroId))
        {
            throw new RegistroNaoLocalizadoException(modelo.LivroId.ToString());
        }

        Avaliacao avaliacao = new Avaliacao(modelo);
        avaliacao.IsValid();
        await _avaliacaoRepository.AddAsync(avaliacao);
        await _avaliacaoRepository.CommitAsync();
        AvaliacaoDto avaliacaoDto = _mapper.Map<Avaliacao, AvaliacaoDto>(avaliacao);
        return new Response<AvaliacaoDto>(avaliacaoDto);
    }

    public async Task<Response<AvaliacaoDto>> AtualizarAsync(AtualizaAvaliacaoModel modelo)
    {
        Avaliacao? avaliacao = await _avaliacaoRepository.ObterPorIdAsync(modelo.Id);
        if (avaliacao is null)
        {
            throw new RegistroNaoLocalizadoException(modelo.Id.ToString());
        }
        avaliacao.Atualizar(modelo);
        avaliacao.IsValid();
        _avaliacaoRepository.Update(avaliacao);
        await _avaliacaoRepository.CommitAsync();
        AvaliacaoDto avaliacaoDto = _mapper.Map<Avaliacao, AvaliacaoDto>(avaliacao);
        return new Response<AvaliacaoDto>(avaliacaoDto);
    }

    public async Task<Response<AvaliacaoDto>> DeletarAsync(int id)
    {
        Avaliacao? avaliacao = await _avaliacaoRepository.ObterPorIdAsync(id);
        if (avaliacao is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        _avaliacaoRepository.Delete(avaliacao);
        await _avaliacaoRepository.CommitAsync();
        return new Response<AvaliacaoDto>();
    }

}
