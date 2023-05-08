using System.ComponentModel.DataAnnotations.Schema;

namespace Gama.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [NotMapped]
        public IEnumerable<UserRoles> UserRoles { get; set; }

        [NotMapped]
        public IEnumerable<User> Users { get; set; }
    }
}
