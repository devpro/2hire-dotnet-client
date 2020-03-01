using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.DependencyInjection
{
    /// <summary>
    /// Service collection. extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the dependency injection configuration.
        /// </summary>
        /// <typeparam name="T">Instance of <see cref="ITwohireRestApiConfiguration"/></typeparam>
        /// <param name="services">Collection of services that will be completed</param>
        /// <returns></returns>
        public static IServiceCollection Add2hireRestApi<T>(this IServiceCollection services, T configuration)
            where T : class, ITwohireRestApiConfiguration
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<ITwohireRestApiConfiguration, T>();
            services.TryAddSingleton<Domain.Providers.ITokenProvider, Providers.TokenProvider>();
            services.TryAddTransient<Domain.Repositories.IPersonalVehicleRepository, Repositories.PersonalVehicleRepository>();
            services.TryAddTransient<Domain.Repositories.ITokenRepository, Repositories.TokenRepository>();

            services
                .AddHttpClient(configuration.HttpClientName, client =>
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            return services;
        }
    }
}
