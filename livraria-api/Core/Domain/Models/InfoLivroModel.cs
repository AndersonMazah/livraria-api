namespace Livraria.Core.Domain.Models;

public class InfoLivroModel
{
    public int LivroId { get; set; }

    public string Descricao { get; set; } = null!;

    public int Paginas { get; set; }

    public string Editora { get; set; } = null!;

}