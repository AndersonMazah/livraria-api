namespace Livraria.Core.Application.Dtos;

public class AvaliacaoDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Nota { get; set; }

    public string Descricao { get; set; } = null!;

}
