using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renting.Microservice.Api.UseCases;
using Renting.Microservice.Api.UseCases.CreateVehicle;
using Renting.Microservice.FunctionalTests.Infrastructure;
using Xunit;

namespace Renting.Microservice.FunctionalTests.Specs
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
