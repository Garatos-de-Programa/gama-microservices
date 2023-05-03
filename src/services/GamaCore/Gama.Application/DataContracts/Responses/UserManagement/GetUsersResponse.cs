namespace Gama.Application.DataContracts.Responses.UserManagement
{
    public class GetUsersResponse
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? DocumentNumber { get; set; }

        public bool Active { get; set; }

        public IEnumerable<string>? Roles { get; set; }
    }
}
