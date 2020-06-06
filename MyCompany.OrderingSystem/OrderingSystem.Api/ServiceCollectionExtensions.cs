using System.Linq;
using AutoMapper;
using CustomerOrders;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderingSystem.WebApi.Controllers;

namespace OrderingSystem.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureWebAPI(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(options =>
                    options.RegisterValidatorsFromAssemblyContaining(typeof(Startup))
                )
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(ms => ms.Value.ValidationState == ModelValidationState.Invalid)
                            .Select(ms => new ValidationFailure(ms.Key, ms.Value.Errors.First().ErrorMessage));

                        return new ObjectResult(errors)
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    };
                });
            return services;
        }
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.ClearPrefixes();
                cfg.CreateMap<OrderPositionDto, ProductQuantity>();
            });
            configuration.CompileMappings();
            configuration.AssertConfigurationIsValid();

            services.AddSingleton(configuration.CreateMapper());
            return services;
        }
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IHostEnvironment environment)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = environment.ApplicationName
                });
            });
            return services;
        }
    }
}
