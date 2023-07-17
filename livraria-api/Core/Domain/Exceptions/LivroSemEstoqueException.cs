namespace Livraria.Core.Domain.Exceptions;

public class LivroSemEstoqueException : Exception
{
    public LivroSemEstoqueException(string mensagem) : base(mensagem) { }
}