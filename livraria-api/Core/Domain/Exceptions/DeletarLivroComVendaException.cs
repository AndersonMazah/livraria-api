namespace Livraria.Core.Domain.Exceptions;

public class DeletarLivroComVendaException : Exception
{
    public DeletarLivroComVendaException(string mensagem) : base(mensagem) { }
}