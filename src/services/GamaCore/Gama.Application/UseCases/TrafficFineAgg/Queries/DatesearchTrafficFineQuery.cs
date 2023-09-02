using Gama.Application.Seedworks.Queries;
using Gama.Domain.Entities.TrafficFinesAgg;

namespace Gama.Application.UseCases.TrafficFineAgg.Queries
{
    internal class DatesearchTrafficFineQuery
    {
        private Func<TrafficFine, bool> _query;

        public DatesearchTrafficFineQuery(bool isCop, DateSearchQuery dateSearchQuery, int userId)
        {
            if (isCop)
            {
                _query = t =>
                t.CreatedAt >= dateSearchQuery.CreatedSince.ToUniversalTime() &&
                t.CreatedAt <= dateSearchQuery.CreatedUntil.ToUniversalTime() &&
                t.UserId == userId;
                return;
            }

            _query = t =>
                t.CreatedAt >= dateSearchQuery.CreatedSince.ToUniversalTime() &&
                t.CreatedAt <= dateSearchQuery.CreatedUntil.ToUniversalTime();
        }

        public bool Filter(TrafficFine trafficFine)
        {
            return _query.Invoke(trafficFine);
        }
    }
}
