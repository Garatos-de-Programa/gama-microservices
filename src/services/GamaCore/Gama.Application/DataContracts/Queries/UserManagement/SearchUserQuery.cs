using Gama.Application.DataContracts.Queries.Common;

namespace Gama.Application.DataContracts.Queries.UserManagement
{
    public class SearchUserQuery : PagedSearchQuery
    {
        public string Role { get; set; }
    }
}
