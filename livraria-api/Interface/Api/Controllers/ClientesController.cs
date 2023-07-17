using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Interface.Api.Controllers;

[ApiController]
[Route("api/cliente")]
public class ClientesController : ControllerBase
{
    private readonly IClientesApplication _clientesApplication;

    public ClientesController(IClientesApplication clientesApplication)
    {
        _clientesApplication = clientesApplication;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ClientesDto>>> ObterPorId(int id)
    {
        try
        {
            return Ok(await _clientesApplication.ObterPorIdAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<ClientesDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Clientes id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Response<Paginator<ClientesDto>>>> ObterTodosClientes([FromQuery] int pageSize, int pageNumber)
    {
        try
        {
            return Ok(await _clientesApplication.ObterTodosAsync(pageSize, pageNumber));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Response<ClientesDto>>> Cadastrar([FromBody] CadastroClienteModel modelo)
    {
        try
        {
            return Ok(await _clientesApplication.CadastrarAsync(modelo));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Response<ClientesDto>>> Atualizar([FromBody] AtualizaClienteModel modelo)
    {
        try
        {
            return Ok(await _clientesApplication.AtualizarAsync(modelo));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<ClientesDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Clientes id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<ClientesDto>>> Delete(int id)
    {
        try
        {
            return Ok(await _clientesApplication.DeletarAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<ClientesDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Clientes id={e.Message} não encontrado"));
        }
        catch (DeletarClienteComVendaException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<ClientesDto>(
                ErrorCodes.REGISTRO_POSSUI_DEPENDENCIAS_E_NAO_PODE_SER_DELETADO,
                $"Registro de Clientes id={e.Message} possui venda e não pode ser deletado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
