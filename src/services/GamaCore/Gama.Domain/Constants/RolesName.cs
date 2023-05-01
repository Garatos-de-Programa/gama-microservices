using Gama.Domain.Entities;

namespace Gama.Domain.Constants;

public static class RolesName
{
    public static readonly Dictionary<string, Role> Roles = new()
    {
        { Cop, new Role()
            {
                Id = 1,
                Name = Cop
            }
        },
        { Admin, new Role()
            {
                Id = 2,
                Name = Admin
            }
        },
        { Citizen, new Role()
            {
                Id = 3,
                Name = Citizen
            }
        },
    };

    public const string Cop = "Cop";
    public const string Admin = "Admin";
    public const string Citizen = "Citizen";
}