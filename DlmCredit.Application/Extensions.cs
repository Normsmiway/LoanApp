using DlmCredit.Application.Disbursement;
using Microsoft.Extensions.DependencyInjection;

namespace DlmCredit.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ILoanDisbursementService, LoanDisbursementService>();
            return services;
        }
    }
}
