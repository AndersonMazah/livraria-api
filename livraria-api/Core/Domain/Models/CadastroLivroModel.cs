namespace Livraria.Core.Domain.Models;

public class CadastroLivroModel
{
    public string Nome { get; set; } = null!;

    public decimal Valor { get; set; }

    public int AutorId { get; set; }
}