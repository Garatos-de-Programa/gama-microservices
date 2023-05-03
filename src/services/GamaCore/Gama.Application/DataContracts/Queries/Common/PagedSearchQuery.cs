namespace Gama.Application.DataContracts.Queries.Common
{
    public class PagedSearchQuery
    {
        public int PageNumber { get; set; } = 1;

        public int Size { get; set; } = 10;
    }
}
