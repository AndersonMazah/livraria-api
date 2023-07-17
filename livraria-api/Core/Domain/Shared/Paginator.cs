namespace Livraria.Core.Domain.Shared;

public class Paginator<T> where T : class
{
    public int PageNumber { get; private set; }

    public int PageSize { get; private set; }

    public int TotalPages { get; private set; }

    public int TotalItems { get; private set; }

    public List<T> Itens { get; private set; }

    public Paginator(int pageSize, int pageNumber, int totalItems, List<T> itens)
    {
        decimal totalPages = Convert.ToDecimal(totalItems / Convert.ToDecimal(pageSize));
        totalPages = Math.Ceiling(totalPages);

        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = decimal.ToInt32(totalPages);
        TotalItems = totalItems;
        Itens = itens;
    }

}
