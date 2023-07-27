using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.Seedworks.Queries
{
    public class PagedSearchQuery : Notifiable<Notification>, IRequest
    {
        public int PageNumber { get; set; } = 1;

        public int Size { get; set; } = 10;

        public virtual void Validate()
        {
            AddNotifications(new PagedSearchQueryContract(this));
        }
    }
}
