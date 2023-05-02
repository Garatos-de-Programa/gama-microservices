using Gama.Domain.Entities;

namespace Gama.Application.DataContracts.Commands.UserManagement;

public class UpdateUserCommand
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string DocumentNumber { get; set; }
}