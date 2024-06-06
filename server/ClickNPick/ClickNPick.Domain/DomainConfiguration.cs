using Microsoft.Extensions.DependencyInjection;

namespace ClickNPick.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}
