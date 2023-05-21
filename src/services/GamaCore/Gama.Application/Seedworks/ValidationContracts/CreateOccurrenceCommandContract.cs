using Flunt.Validations;
using Gama.Application.DataContracts.Commands.OccurrenceManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateOccurrenceCommandContract : Contract<CreateOccurrenceCommand>
    {
        public CreateOccurrenceCommandContract(CreateOccurrenceCommand createOccurrenceCommand)
        {
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Location, nameof(createOccurrenceCommand.Location), "Você deve informar uma localização.");
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Name, nameof(createOccurrenceCommand.Name), "Você deve informar um nome.");
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Description, nameof(createOccurrenceCommand.Description), "Você deve informar uma descrição.");
        }
    }
}
