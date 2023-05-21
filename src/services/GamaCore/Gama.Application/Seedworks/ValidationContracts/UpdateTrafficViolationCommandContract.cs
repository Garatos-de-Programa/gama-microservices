using Flunt.Validations;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;

namespace Gama.Application.Seedworks.ValidationContracts
{
    internal class UpdateTrafficViolationCommandContract : Contract<UpdateTrafficViolationCommand>
    {
        public UpdateTrafficViolationCommandContract(UpdateTrafficViolationCommand updateTrafficViolationCommand)
        {
            IsNotNullOrEmpty(updateTrafficViolationCommand.Name, nameof(updateTrafficViolationCommand.Name));
            IsNotNullOrEmpty(updateTrafficViolationCommand.Code, nameof(updateTrafficViolationCommand.Code));
        }
    }
}
