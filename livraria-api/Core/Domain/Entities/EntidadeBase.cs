namespace Livraria.Core.Domain.Entities;

public abstract class EntidadeBase<T> where T : class
{
    protected virtual void ValidateState()
    {
        return;
    }

    public bool IsValid()
    {
        this.ValidateState();
        return true;
    }
}