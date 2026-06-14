using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;

namespace Renting.Microservice.Api.UseCases.ListAvailableVehicles
{
    public sealed class ListAvailableVehiclesPresenter : IWebApiPresenter, IListAvailableVehiclesOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ListAvailableVehiclesOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var viewModel = new ListAvailableVehiclesResponse
            {
                Vehicles = response.Vehicles
                    .Select(v => new VehicleResponseItem
                    {
                        Id = v.Id,
                        LicensePlate = v.LicensePlate,
                        Brand = v.Brand,
                        Model = v.Model,
                        ManufactureDate = v.ManufactureDate,
                    })
                    .ToList()
                    .AsReadOnly(),
            };

            ActionResult = new OkObjectResult(viewModel);
        }
    }
}
