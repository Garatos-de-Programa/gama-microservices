using Flunt.Validations;
using Gama.Application.UseCases.TrafficFineAgg.Queries;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class TrafficFineListingQueryContract : Contract<TrafficFineListingQuery>
    {
        public TrafficFineListingQueryContract(TrafficFineListingQuery trafficFineListingQuery)
        {
            if (string.IsNullOrWhiteSpace(trafficFineListingQuery.LicensePlate))
            {
                return;
            }

            IsTrue(MercosulLicensePlate.TryParse(trafficFineListingQuery.LicensePlate, out var _), nameof(trafficFineListingQuery.LicensePlate), "Você deve informar uma placa válida.");
        }
    }
}
