using ClickNPick.Application.RecurringJobs;
using ClickNPick.Infrastructure;
using Hangfire;

namespace ClickNPick.StartUp.Configurations;

public static class ApplicationBuilderConfigurations
{
    public static IApplicationBuilder ConfigurePipeline(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("policy-base");
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.ApplyMigrations();

        app.AddHangfireJobs();
        app.AddHangfireDashboard();

        return app;
    }

    private static IApplicationBuilder AddHangfireDashboard(this IApplicationBuilder app)
    {
        var options = new DashboardOptions { AppPath = "http://localhost:3000", DarkModeEnabled = true };
        app.UseHangfireDashboard("/hangfire", options);

        return app;
    }

    private static IApplicationBuilder AddHangfireJobs(this IApplicationBuilder app)
    {
        var recurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
        recurringJobManager.AddOrUpdate<UnpromoteExpiredProductAdsRecurringJob>(
            "Ad un-promoter",
            x => x.StartWorking(null),
            "*/5 * * * *");

        return app;
    }
}
