namespace Livraria.Core.Domain.Models;

public class CadastroAvaliacaoModel
{
    public string Nome { get; set; } = null!;

    public int Nota { get; set; }

    public string Descricao { get; set; } = null!;

    public int LivroId { get; set; }
}