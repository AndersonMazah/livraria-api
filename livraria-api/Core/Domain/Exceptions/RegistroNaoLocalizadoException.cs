namespace Livraria.Core.Domain.Exceptions;

public class RegistroNaoLocalizadoException : Exception
{
    public RegistroNaoLocalizadoException(string mensagem) : base(mensagem) { }
}