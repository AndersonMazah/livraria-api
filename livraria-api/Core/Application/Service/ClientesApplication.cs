using AutoMapper;
using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Service;

public class ClientesApplication : IClientesApplication
{
    private IClientesRepository _clientesRepository;
    private IVendasRepository _vendasRepository;
    private IMapper _mapper;

    public ClientesApplication(
        IMapper mapper,
        IClientesRepository clientesRepository,
        IVendasRepository vendasRepository
    )
    {
        _mapper = mapper;
        _clientesRepository = clientesRepository;
        _vendasRepository = vendasRepository;
    }

    public async Task<Response<ClientesDto>> ObterPorIdAsync(int id)
    {
        Clientes? cliente = await _clientesRepository.ObterPorIdAsync(id);
        if (cliente is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        ClientesDto clientesDto = _mapper.Map<Clientes, ClientesDto>(cliente);
        return new Response<ClientesDto>(clientesDto);
    }

    public async Task<Response<Paginator<ClientesDto>>> ObterTodosAsync(int pageSize, int pageNumber)
    {
        Paginator<Clientes> cliente = await _clientesRepository.ObterTodosAsync(pageSize, pageNumber);
        List<ClientesDto> clientesDto = _mapper.Map<List<Clientes>, List<ClientesDto>>(cliente.Itens);
        Paginator<ClientesDto> paginator = new Paginator<ClientesDto>(cliente.PageSize, cliente.PageNumber, cliente.TotalItems, clientesDto);
        return new Response<Paginator<ClientesDto>>(paginator);
    }

    public async Task<Response<ClientesDto>> CadastrarAsync(CadastroClienteModel modelo)
    {
        Clientes cliente = new Clientes(modelo);
        cliente.IsValid();
        await _clientesRepository.AddAsync(cliente);
        await _clientesRepository.CommitAsync();
        ClientesDto ClientesDto = _mapper.Map<Clientes, ClientesDto>(cliente);
        return new Response<ClientesDto>(ClientesDto);
    }

    public async Task<Response<ClientesDto>> AtualizarAsync(AtualizaClienteModel modelo)
    {
        Clientes? cliente = await _clientesRepository.ObterPorIdAsync(modelo.Id);
        if (cliente is null)
        {
            throw new RegistroNaoLocalizadoException(modelo.Id.ToString());
        }
        cliente.Atualizar(modelo);
        cliente.IsValid();
        _clientesRepository.Update(cliente);
        await _clientesRepository.CommitAsync();
        ClientesDto ClientesDto = _mapper.Map<Clientes, ClientesDto>(cliente);
        return new Response<ClientesDto>(ClientesDto);
    }

    public async Task<Response<ClientesDto>> DeletarAsync(int id)
    {
        Clientes? cliente = await _clientesRepository.ObterPorIdAsync(id);
        if (cliente is null)
        {
            throw new RegistroNaoLocalizadoException(id.ToString());
        }
        if (await _vendasRepository.VerificarSeExisteAlgumaVendaParaOClienteAsync(id))
        {
            throw new DeletarClienteComVendaException(id.ToString());
        }
        _clientesRepository.Delete(cliente);
        await _clientesRepository.CommitAsync();
        return new Response<ClientesDto>();
    }

}
