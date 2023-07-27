using Flunt.Validations;
using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateUserCommandContract : Contract<CreateUserCommand>
    {
        public CreateUserCommandContract(CreateUserCommand createUserCommand)
        {
            IsNotNullOrWhiteSpace(createUserCommand.FirstName, nameof(createUserCommand.FirstName), "Você deve informar o primeiro nome.");
            IsNotNullOrWhiteSpace(createUserCommand.LastName, nameof(createUserCommand.LastName), "Você deve informar o último nome.");
            IsNotNullOrWhiteSpace(createUserCommand.Username, nameof(createUserCommand.Username), "Você deve informar o username.");
            IsTrue(Email.TryParse(createUserCommand.Email, out var _), nameof(createUserCommand.Email), "Você deve informar um e-mail.");
            IsNotNullOrWhiteSpace(createUserCommand.Password, nameof(createUserCommand.Password), "Você deve informar uma senha.");
            IsTrue(Cpf.TryParse(createUserCommand.DocumentNumber, out var _), nameof(createUserCommand.DocumentNumber), "Você deve informar um documento válido.");
        }
    }
}
