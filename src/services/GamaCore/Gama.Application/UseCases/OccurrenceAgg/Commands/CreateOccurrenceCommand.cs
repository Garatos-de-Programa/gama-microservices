using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.UseCases.OccurrenceAgg.Commands
{
    public class CreateOccurrenceCommand : Notifiable<Notification>, IRequest
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Location { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public byte OccurrenceStatusId { get; set; }

        public byte OccurrenceTypeId { get; set; }

        public byte OccurrenceUrgencyLevelId { get; set; }

        public void Validate()
        {
            AddNotifications(new CreateOccurrenceCommandContract(this));
        }
    }
}
