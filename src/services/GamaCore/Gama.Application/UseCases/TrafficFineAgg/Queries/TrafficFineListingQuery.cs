using Gama.Application.Seedworks.Queries;
using Gama.Application.Seedworks.ValidationContracts;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.ValueTypes;
using Gama.Shared.Extensions;
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
            var utcCreatedSince = CreatedSince.ToUtc(DatetimeExtensions.BrazilianTimeZoneId);
            var utcCreatedUntil = CreatedUntil.ToUtc(DatetimeExtensions.BrazilianTimeZoneId);

            if (string.IsNullOrEmpty(LicensePlate))
            {
                if (isCop)
                {
                    return t =>
                    t.CreatedAt >= utcCreatedSince &&
                    t.CreatedAt <= utcCreatedUntil &&
                    t.UserId == userId;
                }

                return t =>
                    t.CreatedAt >= utcCreatedSince &&
                    t.CreatedAt <= utcCreatedUntil;
            }

            var licensePlate = MercosulLicensePlate.Parse(LicensePlate!);

            if (isCop)
            {
                return t =>
                t.CreatedAt >= utcCreatedSince &&
                t.CreatedAt <= utcCreatedUntil &&
                t.UserId == userId && 
                t.LicensePlate!.Equals(licensePlate);
            }

            return t =>
                t.CreatedAt >= utcCreatedSince &&
                t.CreatedAt <= utcCreatedUntil && 
                t.LicensePlate!.Equals(licensePlate);
        }
    }
}
