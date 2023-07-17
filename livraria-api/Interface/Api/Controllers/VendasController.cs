using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Domain.Exceptions;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Interface.Api.Controllers;

[ApiController]
[Route("api/venda")]
public class VendasController : ControllerBase
{
    private readonly IVendasApplication _vendasApplication;

    public VendasController(IVendasApplication vendasApplication)
    {
        _vendasApplication = vendasApplication;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<VendasDto>>> ObterPorId(int id)
    {
        try
        {
            return Ok(await _vendasApplication.ObterPorIdAsync(id));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<VendasDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de Vendas id={e.Message} não encontrado"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Response<Paginator<VendasDto>>>> ObterTodosVendas([FromQuery] int pageSize, int pageNumber, int? clienteId, int? livroId, int? autorId)
    {
        try
        {
            int idCliente = clienteId ?? 0;
            int idLivro = livroId ?? 0;
            int idAutor = autorId ?? 0;
            return Ok(await _vendasApplication.ObterTodosAsync(pageSize, pageNumber, idCliente, idLivro, idAutor));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Response<VendasDto>>> Cadastrar([FromBody] CadastroVendaModel modelo)
    {
        try
        {
            return Ok(await _vendasApplication.CadastrarAsync(modelo));
        }
        catch (RegistroNaoLocalizadoException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<VendasDto>(
                ErrorCodes.REGISTRO_NAO_ENCONTRADO,
                $"Registro de {e.Message} não encontrado"));
        }
        catch (LivroSemEstoqueException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<VendasDto>(
                ErrorCodes.LIVRO_SEM_ESTOQUE,
                $"Registro de Livro id={e.Message} não tem no estoque"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
