namespace Gama.Application.DataContracts.Responses.UserManagement
{
    public class GetUserResonse
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? DocumentNumber { get; set; }

        public bool Active { get; set; }

        public GetUserAddressResponse? Address { get; set; }

        public IEnumerable<string>? Roles { get; set; }
    }
}
