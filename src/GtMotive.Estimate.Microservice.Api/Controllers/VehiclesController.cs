using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator mediator;

        public VehiclesController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request)
        {
            var presenter = await mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            var presenter = await mediator.Send(new ListAvailableVehiclesRequest());
            return presenter.ActionResult;
        }

        [HttpPost("{id:guid}/rent")]
        public async Task<IActionResult> Rent(Guid id, [FromBody] RentVehicleRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            request.VehicleId = id;
            var presenter = await mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpPost("{id:guid}/return")]
        public async Task<IActionResult> Return(Guid id)
        {
            var request = new ReturnVehicleRequest { VehicleId = id };
            var presenter = await mediator.Send(request);
            return presenter.ActionResult;
        }
    }
}
