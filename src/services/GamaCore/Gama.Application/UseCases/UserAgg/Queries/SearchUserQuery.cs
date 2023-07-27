using Flunt.Notifications;
using Gama.Application.Seedworks.Queries;

namespace Gama.Application.UseCases.UserAgg.Queries
{
    public class SearchUserQuery : PagedSearchQuery
    {
        public string Role { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(Role))
            {
                AddNotification(new Notification(nameof(Role), "Você deve informar um cargo para a busca!"));
            }
        }
    }
}
