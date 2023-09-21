using Gama.Application.Seedworks.Queries;
using Gama.Application.Seedworks.ValidationContracts;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.ValueTypes;
using System.Linq.Expressions;

namespace Gama.Application.UseCases.TrafficFineAgg.Queries
{
    public class TrafficFineListingQuery : DateSearchQuery
    {
        public string? LicensePlate { get; init; }

        public override void Validate()
        {
            base.Validate();
            AddNotifications(new TrafficFineListingQueryContract(this));
        }

        internal Expression<Func<TrafficFine, bool>> ToExpression(int userId, bool isCop)
        {
            if (string.IsNullOrEmpty(LicensePlate))
            {
                if (isCop)
                {
                    return t =>
                    t.CreatedAt >= CreatedSince.ToUniversalTime() &&
                    t.CreatedAt <= CreatedUntil.ToUniversalTime() &&
                    t.UserId == userId;
                }

                return t =>
                    t.CreatedAt >= CreatedSince.ToUniversalTime() &&
                    t.CreatedAt <= CreatedUntil.ToUniversalTime();
            }


            if (isCop)
            {
                return t =>
                t.CreatedAt >= CreatedSince.ToUniversalTime() &&
                t.CreatedAt <= CreatedUntil.ToUniversalTime() &&
                t.UserId == userId && 
                t.LicensePlate!.Equals(MercosulLicensePlate.Parse(LicensePlate!));
            }

            return t =>
                t.CreatedAt >= CreatedSince.ToUniversalTime() &&
                t.CreatedAt <= CreatedUntil.ToUniversalTime() && 
                t.LicensePlate!.Equals(MercosulLicensePlate.Parse(LicensePlate!));
        }
    }
}
