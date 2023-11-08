using System.Text;
using Application.Caches.Implementations;
using Application.Caches.Interfaces;
using Auth;
using Auth.Implementations;
using Auth.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using PsychoQuest.Presentation.Constraints;
using Repository.Contracts;
using Repository.DbContext;
using Repository;
using Repository.Initializer;
using Service;
using Service.Contracts;

namespace PsychoQuest.Extensions;

public static class ServiceExtensions
{
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"]
                };
            });

        services.AddIdentity<User, IdentityRole<long>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 4;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        }).AddEntityFrameworkStores<PostgreDbContext>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        
        services.AddScoped<IJwtGenerator, JwtGenerator>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));
    }

    public static void ConfigureDbContext(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<PostgreDbContext>();
    }
    
    public static void ConfigureNlog(this IServiceCollection services)
    {
        services.AddScoped<ILoggerManager, LoggerManager>();
    }
    
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureRouteOptions(this IServiceCollection services)
        => services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap.Add("TypeTest", typeof(TypeTestConstraint));
        });
    
    public static void ConfigureMediatR(this IServiceCollection services) => 
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly));

    public static async void ConfigureIntializer(this IServiceCollection service)
    {
        var listService = service.BuildServiceProvider().CreateScope();
        
        var rolesManager = listService.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole<long>>>();
        var userManager = listService.ServiceProvider
            .GetRequiredService<UserManager<User>>();
        
        await Initializer.InitializerRoleAsync(rolesManager);
        await Initializer.InitializerUserAsync(userManager);
    }

    public static void ConfigureCache(this IServiceCollection services)
    {
        services.AddResponseCaching();
        services.AddSingleton<IMemoryCache, MemoryCache>();
        services.AddScoped(typeof(ICacheManager<>), typeof(CacheManager<>));
    }
}