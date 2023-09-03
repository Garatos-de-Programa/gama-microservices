using Flunt.Validations;
using Gama.Application.UseCases.OccurrenceAgg.Commands;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateOccurrenceCommandContract : Contract<CreateOccurrenceCommand>
    {
        public CreateOccurrenceCommandContract(CreateOccurrenceCommand createOccurrenceCommand)
        {
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Location, nameof(createOccurrenceCommand.Location), "Você deve informar uma localização.");
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Name, nameof(createOccurrenceCommand.Name), "Você deve informar um nome.");
            IsNotNullOrWhiteSpace(createOccurrenceCommand.Description, nameof(createOccurrenceCommand.Description), "Você deve informar uma descrição.");
            IsNotNullOrWhiteSpace(createOccurrenceCommand.ImageUrl, nameof(createOccurrenceCommand.ImageUrl), "Você deve informar uma imagem.");
            IsTrue(S3File.IsValidUrl(createOccurrenceCommand.ImageUrl!), nameof(createOccurrenceCommand.ImageUrl), "Imagem inválida.");
        }
    }
}
