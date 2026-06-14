using Microsoft.Extensions.DependencyInjection;

namespace Renting.Microservice.Infrastructure.Interfaces
{
    public interface IInfrastructureBuilder
    {
        IServiceCollection Services { get; }
    }
}
