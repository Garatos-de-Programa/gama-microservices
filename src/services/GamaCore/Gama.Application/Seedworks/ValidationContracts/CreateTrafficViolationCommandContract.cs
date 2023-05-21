using Flunt.Validations;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateTrafficViolationCommandContract : Contract<CreateTrafficViolationCommand>
    {
        public CreateTrafficViolationCommandContract(CreateTrafficViolationCommand createTrafficViolationCommand)
        {
          IsNotNullOrEmpty(createTrafficViolationCommand.Name, nameof(createTrafficViolationCommand.Name));  
          IsNotNullOrEmpty(createTrafficViolationCommand.Code, nameof(createTrafficViolationCommand.Code));  
        }
    }
}
