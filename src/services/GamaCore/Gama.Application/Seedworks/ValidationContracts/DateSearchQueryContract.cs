using Flunt.Validations;
using Gama.Application.Seedworks.Queries;

namespace Gama.Application.Seedworks.ValidationContracts;

public class DateSearchQueryContract : Contract<DateSearchQuery>
{
    public DateSearchQueryContract(DateSearchQuery search)
    {
        IsMinValue(search.CreatedSince, nameof(search.CreatedSince), "Data de início invalida");
        IsMinValue(search.CreatedUntil, nameof(search.CreatedUntil), "Data de fim invalida");
        IsGreaterThan(search.CreatedSince, search.CreatedUntil, nameof(search.CreatedSince), "Data de início invalida, a data de início deve ser menor que a data fim");
    }
}