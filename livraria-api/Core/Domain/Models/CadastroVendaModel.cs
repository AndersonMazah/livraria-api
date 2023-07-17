namespace Livraria.Core.Domain.Models;

public class CadastroVendaModel
{
    public DateTime Data { get; set; }

    public int ClienteId { get; set; }

    public int LivroId { get; set; }
}
