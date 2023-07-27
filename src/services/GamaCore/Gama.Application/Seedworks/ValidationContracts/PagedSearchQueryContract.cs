using Flunt.Validations;
using Gama.Application.Seedworks.Queries;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class PagedSearchQueryContract : Contract<PagedSearchQuery>
    {
        public PagedSearchQueryContract(PagedSearchQuery pagedSearchQuery)
        {
            IsGreaterOrEqualsThan(pagedSearchQuery.PageNumber, 1, nameof(pagedSearchQuery.PageNumber), "Você deve informar uma página válida");
            IsLowerOrEqualsThan(pagedSearchQuery.Size, 30, nameof(pagedSearchQuery.Size), "Você deve informar um valor menor ou igual a 30");
        }
    }
}
