using Gama.Domain.Entities;

namespace Gama.Application.DataContracts.Commands.UserManagement;

public class UpdateUserCommand
{
    public string FirstName { get; }

    public string LastName { get; }

    public string DocumentNumber { get; }
}