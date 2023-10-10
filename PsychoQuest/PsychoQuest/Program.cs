using Newtonsoft.Json.Converters;
using PsychoQuest.Extensions;
using Repository;
using Repository.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddScoped<ILoggerManager, LoggerManager>();
builder.Services.ConfigureMediatR();

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(PsychoQuest.Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.ConfigureIntializer();
builder.Services.ConfigureRouteOptions();

var app = builder.Build();

app.ConfigureExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();