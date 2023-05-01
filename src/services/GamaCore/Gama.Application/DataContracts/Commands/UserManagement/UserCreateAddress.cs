namespace Gama.Application.DataContracts.Commands.UserManagement
{
    public class UserCreateAddress
    {
        public string? ZipCode { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? District { get; set; }
    }
}
