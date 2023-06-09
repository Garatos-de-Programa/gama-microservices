﻿using Flunt.Validations;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class CreateTrafficFineCommandContract : Contract<CreateTrafficFineCommand>
    {
        public CreateTrafficFineCommandContract(CreateTrafficFineCommand createTrafficFineCommand)
        {
            IsTrue(MercosulLicensePlate.TryParse(createTrafficFineCommand.LicensePlate, out var _), nameof(createTrafficFineCommand.LicensePlate), "Você deve informar uma placa válida.");
            IsNotEmpty(createTrafficFineCommand.TrafficViolations, nameof(createTrafficFineCommand.TrafficViolations), "Você deve informar as infrações.");
        }
    }
}
