using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.DataContracts.Queries.Common;

public class DateSearchQuery : Notifiable<Notification>
{
    public DateTime CreatedSince { get; set; }

    public DateTime CreatedUntil { get; set; }
    
    public DateSearchQuery()
    {
        AddNotifications(new DateSearchQueryContract(this));
    }
}