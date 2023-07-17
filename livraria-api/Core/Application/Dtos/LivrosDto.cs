namespace Livraria.Core.Application.Dtos;

public class LivrosDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Valor { get; set; }

    public int? AutorId { get; set; }
}
