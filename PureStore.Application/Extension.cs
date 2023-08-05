using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PureStore.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });


            return services;
        }
    }
}