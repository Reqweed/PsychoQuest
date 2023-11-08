using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using PsychoQuest.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureNlog();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureCache();

builder.Services.AddAuth(builder.Configuration);

builder.Services.ConfigureIntializer();
builder.Services.ConfigureRouteOptions();

builder.Services.AddControllers(options =>
    {
        options.CacheProfiles.Add("Cache", new CacheProfile()
        {
            Location = ResponseCacheLocation.Any,
            Duration = 60
        });
    })
    .AddApplicationPart(typeof(PsychoQuest.Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

var app = builder.Build();

app.ConfigureExceptionHandler();

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();