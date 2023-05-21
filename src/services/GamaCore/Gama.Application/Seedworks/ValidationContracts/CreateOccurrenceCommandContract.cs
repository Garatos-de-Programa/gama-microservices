using Flunt.Validations;
using Gama.Application.DataContracts.Commands.OccurrenceManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateOccurrenceCommandContract : Contract<CreateOccurrenceCommand>
    {
        public CreateOccurrenceCommandContract(CreateOccurrenceCommand createOccurrenceCommand)
        {
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Location, nameof(createOccurrenceCommand.Location));
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Name, nameof(createOccurrenceCommand.Name));
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Description, nameof(createOccurrenceCommand.Description));
        }
    }
}
