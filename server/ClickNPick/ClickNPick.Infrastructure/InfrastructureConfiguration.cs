using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Services.Payment;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using ClickNPick.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ClickNPick.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRepository();
        serviceCollection.AddService();
        return serviceCollection;
    }

    private static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return serviceCollection;
    }

    private static IServiceCollection AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICacheService, CacheService>();
        serviceCollection.AddScoped<IPaymentService, PaymentService>();
        serviceCollection.AddScoped<ICloudinaryService, CloudinaryService>();
        serviceCollection.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        serviceCollection.AddScoped<IEmailSender, EmailSender>();
        return serviceCollection;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var services = app.ApplicationServices.CreateScope();

        var dbContext = services.ServiceProvider.GetService<ClickNPickDbContext>();

        dbContext.Database.EnsureCreated();
    }
}
