using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Interface.Api.Controllers;

[ApiController]
[Route("api/livro")]
public class LivrosController : ControllerBase
{
    private readonly ILivrosApplication _livrosApplication;
    private readonly IAvaliacaoApplication _avaliacaoApplication;

    public LivrosController(
        ILivrosApplication livrosApplication,
        IAvaliacaoApplication avaliacaoApplication
        )
    {
        _livrosApplication = livrosApplication;
        _avaliacaoApplication = avaliacaoApplication;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<LivrosDto>>> ObterPorId(int id)
    {
        try
        {
            return Ok(await _livrosApplication.ObterPorIdAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<LivrosDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Livro id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Response<Paginator<LivrosDto>>>> ObterTodosLivros([FromQuery] int pageSize, int pageNumber, int? autorId)
    {
        try
        {
            int idAutor = autorId ?? 0;
            return Ok(await _livrosApplication.ObterTodosAsync(pageSize, pageNumber, idAutor));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Response<LivrosDto>>> Cadastrar([FromBody] CadastroLivroModel modelo)
    {
        try
        {
            return Ok(await _livrosApplication.CadastrarAsync(modelo));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Response<LivrosDto>>> Atualizar([FromBody] AtualizaLivroModel modelo)
    {
        try
        {
            return Ok(await _livrosApplication.AtualizarAsync(modelo));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<LivrosDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Livro id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<LivrosDto>>> Delete(int id)
    {
        try
        {
            return Ok(await _livrosApplication.DeletarAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<LivrosDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Livro id={e.Message} não encontrado"));
        }
        catch (DeletarLivroComVendaException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<ClientesDto>(
                ErrorCodes.REGISTRO_POSSUI_DEPENDENCIAS_E_NAO_PODE_SER_DELETADO,
                $"Registro de Livro id={e.Message} possui Venda e não pode ser deletado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("info/{id:int}")]
    public async Task<ActionResult<Response<InfoLivroDto>>> ObterInfoPorId(int id)
    {
        try
        {
            return Ok(await _livrosApplication.ObterInfoPorIdAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<InfoLivroDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Livro id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("info")]
    public async Task<ActionResult<Response<LivrosDto>>> CadastrarInfo([FromBody] InfoLivroModel modelo)
    {
        try
        {
            return Ok(await _livrosApplication.SalvarInfoLivroAsync(modelo));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("info")]
    public async Task<ActionResult<Response<LivrosDto>>> AtualizarInfo([FromBody] InfoLivroModel modelo)
    {
        try
        {
            return Ok(await _livrosApplication.SalvarInfoLivroAsync(modelo));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("info/{id:int}")]
    public async Task<ActionResult<Response<LivrosDto>>> DeletarInfo(int id)
    {
        try
        {
            InfoLivroModel modelo = new InfoLivroModel();
            modelo.LivroId = id;
            return Ok(await _livrosApplication.SalvarInfoLivroAsync(modelo));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{livroID:int}/avaliacao")]
    public async Task<ActionResult<Response<AvaliacaoDto>>> ObterAvaliacoesDoLivroPorId(int livroID)
    {
        try
        {
            return Ok(await _avaliacaoApplication.ObterTodasAvaliacoesDoLivroAsync(livroID));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<AvaliacaoDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Livro id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("{livroID:int}/avaliacao")]
    public async Task<ActionResult<Response<AvaliacaoDto>>> CadastrarAvaliacao([FromBody] CadastroAvaliacaoModel modelo)
    {
        try
        {
            return Ok(await _avaliacaoApplication.CadastrarAsync(modelo));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<AvaliacaoDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Livro id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
