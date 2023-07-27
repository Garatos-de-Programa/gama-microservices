namespace Gama.Application.Seedworks.Pagination;

public class OffsetPageResponse<T>
{
    public int PageNumber { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public IEnumerable<T> Results { get; set; }
}