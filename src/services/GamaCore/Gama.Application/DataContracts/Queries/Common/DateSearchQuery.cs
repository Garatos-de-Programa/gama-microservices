namespace Gama.Application.DataContracts.Queries.Common;

public class DateSearchQuery : PagedSearchQuery
{
    public DateTime CreatedSince { get; set; }

    public DateTime CreatedUntil { get; set; }
}