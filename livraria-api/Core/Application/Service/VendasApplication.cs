using AutoMapper;
using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Service;

public class VendasApplication : IVendasApplication
{
    private IVendasRepository _vendasRepository;
    private ILivrosRepository _livrosRepository;
    private IClientesRepository _clientesRepository;
    private IMapper _mapper;

    public VendasApplication(
        IMapper mapper,
        IVendasRepository vendasRepository,
        ILivrosRepository livrosRepository,
        IClientesRepository clientesRepository
    )
    {
        _mapper = mapper;
        _vendasRepository = vendasRepository;
        _livrosRepository = livrosRepository;
        _clientesRepository = clientesRepository;
    }

    public async Task<Response<VendasDto>> ObterPorIdAsync(int id)
    {
        Vendas? venda = await _vendasRepository.ObterPorIdAsync(id);
        if (venda is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        VendasDto vendasDto = _mapper.Map<Vendas, VendasDto>(venda);
        return new Response<VendasDto>(vendasDto);
    }

    public async Task<Response<Paginator<VendasDto>>> ObterTodosAsync(int pageSize, int pageNumber, int idCliente, int idLivro, int idAutor)
    {
        Paginator<Vendas> venda = await _vendasRepository.ObterTodosAsync(pageSize, pageNumber, idCliente, idLivro, idAutor);
        List<VendasDto> vendasDto = _mapper.Map<List<Vendas>, List<VendasDto>>(venda.Itens);
        Paginator<VendasDto> paginator = new Paginator<VendasDto>(venda.PageSize, venda.PageNumber, venda.TotalItems, vendasDto);
        return new Response<Paginator<VendasDto>>(paginator);
    }

    public async Task<Response<VendasDto>> CadastrarAsync(CadastroVendaModel modelo)
    {
        Livros? livro = await _livrosRepository.ObterPorIdAsync(modelo.LivroId);
        if (livro is null)
        {
            throw new RegistroNaoLocalizadoException($"Livro id={modelo.LivroId}");
        }
        if (livro.Estoque == 0)
        {
            throw new LivroSemEstoqueException(modelo.LivroId.ToString());
        }

        if (!await _clientesRepository.VerificarSeClienteExiste(modelo.ClienteId))
        {
            throw new RegistroNaoLocalizadoException($"Cliente id={modelo.ClienteId}");
        }

        Vendas venda = new Vendas(modelo, livro.Valor);
        venda.IsValid();
        await _vendasRepository.AddAsync(venda);
        await _vendasRepository.CommitAsync();

        livro.DiminuirEstoque();
        _livrosRepository.Update(livro);
        await _livrosRepository.CommitAsync();

        VendasDto VendasDto = _mapper.Map<Vendas, VendasDto>(venda);
        return new Response<VendasDto>(VendasDto);
    }

}
