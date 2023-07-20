using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;
using Livraria.Infra.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Interface.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/autor")]
public class AutoresController : ControllerBase
{
    private readonly IAutoresApplication _autoresApplication;

    public AutoresController(IAutoresApplication autoresApplication)
    {
        _autoresApplication = autoresApplication;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<AutoresDto>>> ObterPorId(int id)
    {
        try
        {
            return Ok(await _autoresApplication.ObterPorIdAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<AutoresDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Autor id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    //[AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<Response<Paginator<AutoresDto>>>> ObterTodosAutores([FromQuery] int pageSize, int pageNumber)
    {
        try
        {
            return Ok(await _autoresApplication.ObterTodosAsync(pageSize, pageNumber));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Response<AutoresDto>>> Cadastrar([FromBody] CadastroAutorModel modelo)
    {
        try
        {
            return Ok(await _autoresApplication.CadastrarAsync(modelo));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Response<AutoresDto>>> Atualizar([FromBody] AtualizaAutorModel modelo)
    {
        try
        {
            return Ok(await _autoresApplication.AtualizarAsync(modelo));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<AutoresDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Autor id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<AutoresDto>>> Delete(int id)
    {
        try
        {
            return Ok(await _autoresApplication.DeletarAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<AutoresDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Autor id={e.Message} não encontrado"));
        }
        catch (DeletarAutorComLivroException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<ClientesDto>(
                ErrorCodes.REGISTRO_POSSUI_DEPENDENCIAS_E_NAO_PODE_SER_DELETADO,
                $"Registro de Autor id={e.Message} possui Livro e não pode ser deletado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
