using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    public sealed class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        public string LicensePlate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        required public System.DateTime ManufactureDate { get; set; }
    }
}
