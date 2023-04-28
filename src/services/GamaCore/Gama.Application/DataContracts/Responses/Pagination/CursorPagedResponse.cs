namespace Gama.Application.DataContracts.Responses.Pagination;

public class CursorPagedResponse<T>
{
    public IEnumerable<T> Result { get; set; }
}