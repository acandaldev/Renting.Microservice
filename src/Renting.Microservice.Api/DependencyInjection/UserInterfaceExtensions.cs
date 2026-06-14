using Microsoft.Extensions.DependencyInjection;
using Renting.Microservice.Api.UseCases.CreateVehicle;
using Renting.Microservice.Api.UseCases.ListAvailableVehicles;
using Renting.Microservice.Api.UseCases.RentVehicle;
using Renting.Microservice.Api.UseCases.ReturnVehicle;
using Renting.Microservice.ApplicationCore.UseCases.CreateVehicle;
using Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using Renting.Microservice.ApplicationCore.UseCases.RentVehicle;
using Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle;

namespace Renting.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            // CreateVehicle
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<ICreateVehicleOutputPort>(sp => sp.GetRequiredService<CreateVehiclePresenter>());

            // ListAvailableVehicles
            services.AddScoped<ListAvailableVehiclesPresenter>();
            services.AddScoped<IListAvailableVehiclesOutputPort>(sp => sp.GetRequiredService<ListAvailableVehiclesPresenter>());

            // RentVehicle
            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<IRentVehicleOutputPort>(sp => sp.GetRequiredService<RentVehiclePresenter>());

            // ReturnVehicle
            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<IReturnVehicleOutputPort>(sp => sp.GetRequiredService<ReturnVehiclePresenter>());

            return services;
        }
    }
}
