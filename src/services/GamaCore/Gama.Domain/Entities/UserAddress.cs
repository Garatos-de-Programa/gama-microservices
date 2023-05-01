namespace Gama.Domain.Entities
{
    public class UserAddress
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? ZipCode { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? District { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        public User? User { get; set; }
    }
}
