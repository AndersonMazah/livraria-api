namespace Livraria.Core.Application.Dtos;

public class VendasDto
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public DateTime Data { get; set; }

    public int? ClienteId { get; set; }

    public int? LivroId { get; set; }
}
