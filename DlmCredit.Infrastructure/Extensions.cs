using System;
using Microsoft.Extensions.Configuration;
using DlmCredit.Infrastructure.ThirdpartyApis;
using Microsoft.Extensions.DependencyInjection;
using DlmCredit.Infrastructure.Services;
using DlmCredit.Application.Contracts;

namespace DlmCredit.Infrastructure
{
    public static class Extensions
    {
        static readonly string monoSection = "MonoConfig";
        static readonly string _monoBaseUrl = "MonoConfig:Url";
        static readonly string _timeOut = "MonoConfig:TimeOut";
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            int timeOut = 30;
            _ = int.TryParse(configuration[_timeOut], out timeOut);
            services.AddHttpClient("MonoClient", config =>
            {
                config.BaseAddress = new Uri(configuration[_monoBaseUrl]);
                config.Timeout = TimeSpan.FromSeconds(timeOut);
            });
            services.Configure<MonoConfig>(configuration.GetSection(monoSection));
            services.AddScoped<IMonoClinet, MonoClinet>();
            services.AddTransient<IIncomeRetrieverService, MonoIncomeService>();
            return services;
        }
    }
}
