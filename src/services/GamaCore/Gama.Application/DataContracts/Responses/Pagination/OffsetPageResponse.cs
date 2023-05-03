namespace Gama.Application.DataContracts.Responses.Pagination;

public class OffsetPageResponse<T>
{
    public int PageNumber { get; set; }

    public int Size { get; set; }

    public IEnumerable<T> Results { get; set; }
}