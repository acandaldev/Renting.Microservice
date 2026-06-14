using System;
using Microsoft.AspNetCore.Mvc;
using Renting.Microservice.ApplicationCore.UseCases.RentVehicle;

namespace Renting.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RentVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(new RentVehicleResponse
            {
                RentalId = response.RentalId,
                VehicleId = response.VehicleId,
                StartDate = response.StartDate,
            });
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }
    }
}
