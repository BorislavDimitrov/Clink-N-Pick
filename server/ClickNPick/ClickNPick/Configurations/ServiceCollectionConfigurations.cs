using ClickNPick.Application;
using ClickNPick.Application.Services.Delivery;
using ClickNPick.Domain;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Web;
using ClickNPick.Web.Validations.Users;
using CloudinaryDotNet;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Stripe;
using System.Net.Http.Headers;
using System.Text;
using Account = CloudinaryDotNet.Account;

namespace ClickNPick.StartUp.Configurations;

public static class ServiceCollectionConfigurations
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDatabaseConfiguration(configuration);

        serviceCollection.AddMemoryCache();

        serviceCollection.AddApplication();
        serviceCollection.AddDomain();
        serviceCollection.AddInfrastructure();
        serviceCollection.AddWeb();
        serviceCollection.AddCloudinaryConfiguration(configuration);
        serviceCollection.AddStripe(configuration);
        serviceCollection.AddSerilog(configuration);
        serviceCollection.AddHangfire(configuration);

        serviceCollection.AddHttpClient();
        serviceCollection.RegisterDeliveryClient<IDeliveryService, DeliveryService>(configuration, "EcontConfiguration");

        serviceCollection.AddIdentityConfiguration();
        serviceCollection.AddAuthenticationConfiguration(configuration);
        serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
        serviceCollection.AddControllersConfiguration();
        serviceCollection.AddCorsConfiguration();

        serviceCollection.AddExceptionHandler<GlobalExceptionHandler>();

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();

        return serviceCollection;
    }

    private static IServiceCollection AddDatabaseConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        serviceCollection.AddDbContext<ClickNPickDbContext>(options =>
            options.UseSqlServer(connectionString));

        return serviceCollection;
    }

    private static IServiceCollection AddCloudinaryConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var cloudinaryCredentials = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]);

        var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

        serviceCollection.AddSingleton(cloudinaryUtility);

        return serviceCollection;
    }

    private static IServiceCollection AddStripe(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

        return serviceCollection;
    }

    private static IServiceCollection AddSerilog(this IServiceCollection servicesCollection, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
        .ReadFrom
        .Configuration(configuration)
        .CreateLogger();

        servicesCollection.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        return servicesCollection;
    }

    private static IServiceCollection AddHangfire(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddHangfire(config => config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(
            configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true,
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
            }).UseConsole())
            .AddHangfireServer(c => c.WorkerCount = 2);
        return serviceCollection;
    }

    private static IServiceCollection AddIdentityConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._ ";
        })
         .AddRoles<IdentityRole>()
         .AddSignInManager()
         .AddEntityFrameworkStores<ClickNPickDbContext>()
         .AddDefaultTokenProviders();

        return serviceCollection;
    }

    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
          {
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                  ValidateIssuer = true,
                  ValidIssuer = configuration["Jwt:Issuer"],
                  ValidateAudience = true,
                  ValidAudience = configuration["Jwt:Audience"],
                  ValidateLifetime = true,
              };
          });

        return serviceCollection;
    }

    private static IServiceCollection AddControllersConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers()
         .AddFluentValidation(fv =>
         {
             fv.RegisterValidatorsFromAssemblyContaining<LoginRequestModelValidator>();
             fv.DisableDataAnnotationsValidation = true;
         });

        serviceCollection.Configure<ApiBehaviorOptions>(options =>
        {
        });

        return serviceCollection;
    }

    private static IServiceCollection AddCorsConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(opt => opt.AddPolicy("policy-base", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }));

        return serviceCollection;
    }

    public static IHttpClientBuilder RegisterDeliveryClient<TInterface, TImplementation>(
       this IServiceCollection services,
       IConfiguration configuration,
       string configurationSectionName)
           where TInterface : class
           where TImplementation : class, TInterface
               => services.AddHttpClient<TInterface, TImplementation>(httpClient =>
               {
                   var clientConfiguration = configuration.GetSection(configurationSectionName);

                   var baseUrl = clientConfiguration["BaseUrl"];
                   var authType = clientConfiguration["AuthType"];

                   if (string.IsNullOrEmpty(baseUrl))
                   {
                       throw new InvalidOperationException($"Base URL for {configurationSectionName} is missing.");
                   }

                   httpClient.BaseAddress = new Uri(baseUrl);

                   if (!string.IsNullOrEmpty(authType))
                   {
                       switch (authType.ToLower())
                       {
                           case "basic":
                               var username = clientConfiguration["Username"];
                               var password = clientConfiguration["Password"];

                               if (string.IsNullOrEmpty(username) ||
                                   string.IsNullOrEmpty(password))
                               {
                                   throw new InvalidOperationException($"Username or password for {configurationSectionName} is missing.");
                               }

                               var credentialsByteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                               httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentialsByteArray));

                               break;

                           default: throw new InvalidOperationException($"Authentication type '{authType}' for {configurationSectionName} is not supported.");
                       }
                   }
               });
}

