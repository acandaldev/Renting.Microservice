using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Renting.Microservice.Api.Authorization;
using Renting.Microservice.Api.DependencyInjection;
using Renting.Microservice.Api.Filters;
using Renting.Microservice.ApplicationCore;

[assembly: CLSCompliant(false)]

namespace Renting.Microservice.Api
{
    [ExcludeFromCodeCoverage]
    public static class ApiConfiguration
    {
        public static void ConfigureControllers(MvcOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            options.Filters.Add<BusinessExceptionFilter>();
        }

        public static IMvcBuilder WithApiControllers(this IMvcBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.AddApplicationPart(typeof(ApiConfiguration).GetTypeInfo().Assembly);

            AddApiDependencies(builder.Services);

            return builder;
        }

        public static void AddApiDependencies(this IServiceCollection services)
        {
            services.AddAuthorization(AuthorizationOptionsExtensions.Configure);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApiConfiguration).GetTypeInfo().Assembly));
            services.AddUseCases();
            services.AddPresenters();
        }
    }
}
