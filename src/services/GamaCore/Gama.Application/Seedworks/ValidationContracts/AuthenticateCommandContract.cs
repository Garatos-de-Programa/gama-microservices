using Flunt.Validations;
using Gama.Application.DataContracts.Commands.UserManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class AuthenticateCommandContract : Contract<AuthenticateCommand>
    {
        public AuthenticateCommandContract(AuthenticateCommand authenticateCommand)
        {
            IsNotNullOrWhiteSpace(authenticateCommand.Login, nameof(authenticateCommand.Login));
            IsNotNullOrWhiteSpace(authenticateCommand.Password, nameof(authenticateCommand.Password));
        }
    }
}
