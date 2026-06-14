using System;
using Microsoft.AspNetCore.Mvc;
using Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle;

namespace Renting.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehiclePresenter : IWebApiPresenter, IReturnVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ReturnVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(new ReturnVehicleResponse
            {
                VehicleId = response.VehicleId,
                EndDate = response.EndDate,
            });
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }
    }
}
