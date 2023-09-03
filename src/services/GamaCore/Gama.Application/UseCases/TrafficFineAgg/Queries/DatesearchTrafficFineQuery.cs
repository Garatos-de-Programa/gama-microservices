using Gama.Application.Seedworks.Queries;
using Gama.Domain.Entities.TrafficFinesAgg;
using System.Linq.Expressions;

namespace Gama.Application.UseCases.TrafficFineAgg.Queries
{
    internal class DatesearchTrafficFineQuery
    {
        public Expression<Func<TrafficFine, bool>> Query;

        public DatesearchTrafficFineQuery(bool isCop, DateSearchQuery dateSearchQuery, int userId)
        {
            if (isCop)
            {
                Query = t =>
                t.CreatedAt >= dateSearchQuery.CreatedSince.ToUniversalTime() &&
                t.CreatedAt <= dateSearchQuery.CreatedUntil.ToUniversalTime() &&
                t.UserId == userId;
                return;
            }

            Query = t =>
                t.CreatedAt >= dateSearchQuery.CreatedSince.ToUniversalTime() &&
                t.CreatedAt <= dateSearchQuery.CreatedUntil.ToUniversalTime();
        }
    }
}
