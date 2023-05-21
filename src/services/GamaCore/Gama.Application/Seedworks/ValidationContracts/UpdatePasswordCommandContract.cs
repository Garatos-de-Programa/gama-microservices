using Flunt.Validations;
using Gama.Application.DataContracts.Commands.UserManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class UpdatePasswordCommandContract : Contract<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandContract(UpdatePasswordCommand updatePasswordCommand)
        {
            IsNotNullOrWhiteSpace(updatePasswordCommand.Login, nameof(updatePasswordCommand.Login));
            IsNotNullOrWhiteSpace(updatePasswordCommand.NewPassword, nameof(updatePasswordCommand.NewPassword));
            IsNotNullOrWhiteSpace(updatePasswordCommand.OldPassword, nameof(updatePasswordCommand.OldPassword));
        }
    }
}
