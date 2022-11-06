using System.Linq;
using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddDbContext<StoreContext>(x =>
        {
            x.UseMySql(configuration.GetConnectionString("SQLiteDefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("SQLiteDefaultConnection")));
        });
        services.AddDbContext<AppIdentityDbContext>(x =>
        {
            x.UseMySql(configuration.GetConnectionString("SQLiteIdentityConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("SQLiteIdentityConnection")));
        });

        /*services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var redisConfiguration = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(redisConfiguration);
        });*/

        services.AddApplicationServices();
        services.AddIdentityServices(configuration);
        services.AddSWaggerDocumintation();
        services.AddCors(option =>
        {
            option.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
        });
        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState.Where(e => e.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors).Select(m => m.ErrorMessage).ToList();
                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}