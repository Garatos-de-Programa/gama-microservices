using Flunt.Validations;
using Gama.Application.DataContracts.Queries.Common;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class PagedSearchQueryContract : Contract<PagedSearchQuery>
    {
        public PagedSearchQueryContract(PagedSearchQuery pagedSearchQuery)
        {
            IsGreaterOrEqualsThan(pagedSearchQuery.PageNumber, 1, "Você deve informar uma página válida");
            IsLowerOrEqualsThan(pagedSearchQuery.Size, 30, "Você deve informar um valor menor ou igual a 30");
        }
    }
}
