using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PureStore.Application.Interfaces.Identity;
using PureStore.Application.Interfaces.Repositories;
using PureStore.Application.Interfaces.Services;
using PureStore.Domain.Common;
using PureStore.Persistence.Contexts;
using PureStore.Persistence.Helpers;
using PureStore.Persistence.Identity.Models;
using PureStore.Persistence.Identity.Services;
using PureStore.Persistence.Repositories;
using PureStore.Persistence.Services;

namespace PureStore.Persistence;

public static class Extension
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSettings(configuration);


        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(configuration.GetConnectionString("Mongo"), Constants.DATABASE_NAME)
            .AddDefaultTokenProviders();

        services.AddTransient<IMongoContext, MongoContext>();

        #region Repositories registrations
        services.AddTransient<IUploadedApplicationRepositoryAsync, UploadedApplicationRepositoryAsync>();
        services.AddTransient<IFeedbackRepositoryAsync, FeedbackRepositoryAsync>();
        services.AddTransient<IDownloadingRepository, DownloadingRepository>();
        #endregion

        services.RegisterServices();

        return services;
    }

    /// <summary>
    /// Register the application modules services
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IUploadApplicationService, UploadApplicationService>();
    }

    /// <summary>
    /// Configure all settings needed in the application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<DBSetting>(setting =>
        {
            setting.ConnectionString = configuration.GetConnectionString("Mongo");
            setting.DatabaseName = Constants.DATABASE_NAME;
        });

        services.Configure<JWTSettings>(jwt =>
        {
            jwt.Issuer = configuration["JWT:Issuer"];
            jwt.DurationInMinutes = int.Parse(configuration["JWT:Issuer"]);
            jwt.Audience = configuration["JWT:Audience"];
            jwt.Key = configuration["JWT:Key"];
        });

        services.Configure<Keys>(key =>
        {
            key.CryptorKey = configuration["Cryptor:Key"];
        });

        return services;
    }
}