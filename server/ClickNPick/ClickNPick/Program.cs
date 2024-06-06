using ClickNPick.StartUp.Configurations;
using Hangfire;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

services.ConfigureServices(configuration);


host.UseSerilog((context, cfg) => cfg.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();


app.ConfigurePipeline(app.Environment);

app.Run();

