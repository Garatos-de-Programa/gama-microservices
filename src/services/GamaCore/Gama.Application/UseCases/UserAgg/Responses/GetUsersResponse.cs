namespace Gama.Application.UseCases.UserAgg.Responses
{
    public class GetUsersResponse
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? DocumentNumber { get; set; }

        public bool Active { get; set; }
    }
}
