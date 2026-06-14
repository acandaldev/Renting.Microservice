using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Renting.Microservice.Domain.Interfaces;
using Renting.Microservice.Infrastructure.Interfaces;
using Renting.Microservice.Infrastructure.Logging;
using Renting.Microservice.Infrastructure.MongoDb;
using Renting.Microservice.Infrastructure.Repositories;
using Renting.Microservice.Infrastructure.Telemetry;

[assembly: CLSCompliant(false)]

namespace Renting.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            // MongoDB
            services.AddSingleton<MongoService>();

            // Repositories
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            return new InfrastructureBuilder(services);
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
