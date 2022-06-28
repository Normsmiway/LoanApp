using Microsoft.Extensions.DependencyInjection;

namespace DlmCredit.Infrastructure.OAuth
{
    public static class Extensions
    {
        public static IServiceCollection AddOAuth(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            return services;
        }
    }
}
