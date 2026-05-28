using System;
using System.Linq;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ListAvailableVehicles
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
