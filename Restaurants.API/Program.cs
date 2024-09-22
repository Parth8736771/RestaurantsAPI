using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration
    .ReadFrom.Configuration(context.Configuration)

// configuration
//.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
//WriteTo.File("Logs/Restaurant-API-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
//.WriteTo.Console(outputTemplate: "[{Timestamp: ddMM HH:mm:ss} {Level:u3}] | {SourceContext} | {NewLine} {Message:lj}{NewLine}{Exception}")
);

var app = builder.Build();

// Adding Seeder data
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();