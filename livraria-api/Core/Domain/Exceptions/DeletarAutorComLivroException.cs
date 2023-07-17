namespace Livraria.Core.Domain.Exceptions;

public class DeletarAutorComLivroException : Exception
{
    public DeletarAutorComLivroException(string mensagem) : base(mensagem) { }
}