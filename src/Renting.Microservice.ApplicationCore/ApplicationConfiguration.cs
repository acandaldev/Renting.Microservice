using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Renting.Microservice.ApplicationCore.UseCases.CreateVehicle;
using Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using Renting.Microservice.ApplicationCore.UseCases.RentVehicle;
using Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle;

[assembly: CLSCompliant(false)]

namespace Renting.Microservice.ApplicationCore
{
    /// <summary>
    /// Registers application use cases in the DI container.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateVehicleUseCase, CreateVehicleUseCase>();
            services.AddScoped<IListAvailableVehiclesUseCase, ListAvailableVehiclesUseCase>();
            services.AddScoped<IRentVehicleUseCase, RentVehicleUseCase>();
            services.AddScoped<IReturnVehicleUseCase, ReturnVehicleUseCase>();

            return services;
        }
    }
}
