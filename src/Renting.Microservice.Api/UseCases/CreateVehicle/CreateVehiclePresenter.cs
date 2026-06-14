using System;
using Microsoft.AspNetCore.Mvc;
using Renting.Microservice.ApplicationCore.UseCases.CreateVehicle;

namespace Renting.Microservice.Api.UseCases.CreateVehicle
{
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(CreateVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new CreatedResult(string.Empty, new CreateVehicleResponse
            {
                VehicleId = response.VehicleId,
            });
        }
    }
}
