using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Renting.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace Renting.Microservice.InfrastructureTests.Specs
{
    public sealed class CreateVehicleEndpointTests : IDisposable
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public CreateVehicleEndpointTests()
        {
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("IntegrationTest")
                .UseStartup<Startup>();

            server = new TestServer(hostBuilder);
            client = server.CreateClient();
        }

        [Fact]
        public async Task PostVehicleWithNullBodyShouldReturnBadRequest()
        {
            using var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            content.Headers.ContentLength = 0;

            var response = await client.PostAsync(
                new Uri("/api/vehicles", UriKind.Relative),
                content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public void Dispose()
        {
            client?.Dispose();
            server?.Dispose();
        }
    }
}
