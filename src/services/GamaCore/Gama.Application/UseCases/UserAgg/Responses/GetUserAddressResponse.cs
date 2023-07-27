namespace Gama.Application.UseCases.UserAgg.Responses
{
    public class GetUserAddressResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? ZipCode { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? District { get; set; }
    }
}
