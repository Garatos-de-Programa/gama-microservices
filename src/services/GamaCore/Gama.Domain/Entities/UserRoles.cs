using System.ComponentModel.DataAnnotations.Schema;

namespace Gama.Domain.Entities
{
    public class UserRoles
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        [NotMapped]
        public Role Role { get; set; }
    }
}
