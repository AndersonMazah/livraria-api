namespace Livraria.Core.Domain.Exceptions;

public class DeletarClienteComVendaException : Exception
{
    public DeletarClienteComVendaException(string mensagem) : base(mensagem) { }
}