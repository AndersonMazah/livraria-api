namespace Livraria.Core.Domain.Shared;

public enum ErrorCodes
{
    REGISTRO_NAO_ENCONTRADO = 1,
    REGISTRO_POSSUI_DEPENDENCIAS_E_NAO_PODE_SER_DELETADO = 2,
    LIVRO_SEM_ESTOQUE = 3
}