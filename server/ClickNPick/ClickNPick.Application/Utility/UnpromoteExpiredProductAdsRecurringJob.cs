using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Domain.Models;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Application.RecurringJobs;

public class UnpromoteExpiredProductAdsRecurringJob
{
    private readonly IRepository<Product> _productRepository;

    public UnpromoteExpiredProductAdsRecurringJob(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    [AutomaticRetry(Attempts = 2)]
    public async Task StartWorking(PerformContext context)
    {
        var products = await _productRepository
            .All()
            .Where(x => x.IsPromoted == true && x.PromotedUntil <= DateTime.UtcNow)
            .ToListAsync();

        foreach (var product in products.WithProgress(context.WriteProgressBar()))
        {
            product.IsPromoted = false;
            product.PromotedUntil = null;
                          
            context.WriteLine($"Successfully un-promoted ad with title: {product.Title}");
        }

        await _productRepository.SaveChangesAsync();
    }
}
