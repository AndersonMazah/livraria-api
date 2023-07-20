namespace Livraria.Core.Application.Dtos;

public class LivrosDetalheDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Valor { get; set; }

    public int? AutorId { get; set; }

    public string? Descricao { get; set; }

    public int? Paginas { get; set; }

    public string? Editora { get; set; }
}
