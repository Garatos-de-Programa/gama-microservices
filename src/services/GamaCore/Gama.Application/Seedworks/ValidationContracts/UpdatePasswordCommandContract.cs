using Flunt.Validations;
using Gama.Application.DataContracts.Commands.UserManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class UpdatePasswordCommandContract : Contract<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandContract(UpdatePasswordCommand updatePasswordCommand)
        {
            IsNotNullOrWhiteSpace(updatePasswordCommand.Login, nameof(updatePasswordCommand.Login), "Você deve informar um login.");
            IsNotNullOrWhiteSpace(updatePasswordCommand.NewPassword, nameof(updatePasswordCommand.NewPassword), "Você deve informar uma nova senha.");
            IsNotNullOrWhiteSpace(updatePasswordCommand.OldPassword, nameof(updatePasswordCommand.OldPassword), "Você deve informar a senha antiga.");
        }
    }
}
