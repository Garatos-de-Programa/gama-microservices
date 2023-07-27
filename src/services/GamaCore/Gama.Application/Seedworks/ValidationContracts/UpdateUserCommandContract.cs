using Flunt.Validations;
using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class UpdateUserCommandContract : Contract<UpdateUserCommand>
    {
        public UpdateUserCommandContract(UpdateUserCommand updateUserCommand)
        {
            IsNotNullOrWhiteSpace(updateUserCommand.LastName, nameof(updateUserCommand.LastName), "Você deve informar o último nome.");
            IsNotNullOrWhiteSpace(updateUserCommand.FirstName, nameof(updateUserCommand.FirstName), "Você deve informar o primeiro nome.");
            IsTrue(Cpf.TryParse(updateUserCommand.DocumentNumber, out var _), nameof(updateUserCommand.DocumentNumber), "Você deve informar um documento válido.");
        }
    }
}
