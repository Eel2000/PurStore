﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PureStore.Application.Interfaces.Repositories;
using PureStore.Domain.Common;
using PureStore.Persistence.Contexts;
using PureStore.Persistence.Helpers;
using PureStore.Persistence.Repositories;

namespace PureStore.Persistence;

public static class Extension
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSettings(configuration);

        services.AddTransient<IMongoContext, MongoContext>();

        #region Repositories registrations
        services.AddTransient<IUploadedApplicationRepositoryAsync, UploadedApplicationRepositoryAsync>();
        services.AddTransient<IFeedbackRepositoryAsync, FeedbackRepositoryAsync>();
        #endregion

        return services;
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
            setting.ConnectionString = configuration.GetConnectionString("Default");
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