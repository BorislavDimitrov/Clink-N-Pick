using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Services.Categories;
using ClickNPick.Application.Services.Comments;
using ClickNPick.Application.Services.Delivery;
using ClickNPick.Application.Services.Identity;
using ClickNPick.Application.Services.Images;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ClickNPick.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddService();
        return serviceCollection;
    }

    private static IServiceCollection AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPromotionPricingService, PromotionPricingService>();
        serviceCollection.AddScoped<ICategoriesService, CategoriesService>();
        serviceCollection.AddScoped<ICommentsService, CommentsService>();
        serviceCollection.AddScoped<IUsersService, UsersService>();
        serviceCollection.AddScoped<IImagesService, ImagesService>();
        serviceCollection.AddScoped<IIdentityService, IdentityService>();
        serviceCollection.AddScoped<IProductsService, ProductsService>();
        serviceCollection.AddScoped<IDeliveryService, DeliveryService>();
        return serviceCollection;
    }
}
