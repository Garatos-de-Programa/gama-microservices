using Flunt.Validations;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class UpdateUserCommandContract : Contract<UpdateUserCommand>
    {
        public UpdateUserCommandContract(UpdateUserCommand updateUserCommand)
        {
            IsNotNullOrWhiteSpace(updateUserCommand.LastName, nameof(updateUserCommand.LastName));
            IsNotNullOrWhiteSpace(updateUserCommand.FirstName, nameof(updateUserCommand.FirstName));
            IsTrue(Cpf.TryParse(updateUserCommand.DocumentNumber, out var _), nameof(updateUserCommand.DocumentNumber));
        }
    }
}
