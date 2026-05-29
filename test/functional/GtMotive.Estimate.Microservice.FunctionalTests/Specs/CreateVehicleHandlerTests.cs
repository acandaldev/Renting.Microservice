using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    [Collection(TestCollections.Functional)]
    public sealed class CreateVehicleHandlerTests(CompositionRootTestFixture fixture)
    {
        [Fact]
        public async Task HandleShouldReturnCreatedResult()
        {
            await fixture.UsingHandlerForRequestResponse<CreateVehicleRequest, IWebApiPresenter>(
                async handler =>
                {
                    var request = new CreateVehicleRequest
                    {
                        LicensePlate = "FUNC001",
                        Brand = "Renault",
                        Model = "Clio",
                        ManufactureDate = System.DateTime.UtcNow.AddYears(-1),
                    };

                    var presenter = await handler.Handle(request, System.Threading.CancellationToken.None);

                    Assert.NotNull(presenter);
                    Assert.IsType<CreatedResult>(presenter.ActionResult);
                });
        }
    }
}
