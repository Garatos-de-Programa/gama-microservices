using Flunt.Validations;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateUserCommandContract : Contract<CreateUserCommand>
    {
        public CreateUserCommandContract(CreateUserCommand createUserCommand)
        {
            IsNotNullOrWhiteSpace(createUserCommand.FirstName, nameof(createUserCommand.FirstName));
            IsNotNullOrWhiteSpace(createUserCommand.LastName, nameof(createUserCommand.LastName));
            IsNotNullOrWhiteSpace(createUserCommand.Username, nameof(createUserCommand.Username));
            IsTrue(Email.TryParse(createUserCommand.Email, out var _), nameof(createUserCommand.Email));
            IsNotNullOrWhiteSpace(createUserCommand.Password, nameof(createUserCommand.Password));
            IsTrue(Cpf.TryParse(createUserCommand.DocumentNumber, out var _), nameof(createUserCommand.DocumentNumber));
        }
    }
}
