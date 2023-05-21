using Flunt.Notifications;
using Gama.Application.DataContracts.Queries.Common;

namespace Gama.Application.DataContracts.Queries.UserManagement
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
