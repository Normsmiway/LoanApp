using System.Threading.Tasks;
using DlmCredit.Application.Contracts;
using DlmCredit.Infrastructure.ThirdpartyApis;

namespace DlmCredit.Infrastructure.Services
{
    internal class MonoIncomeService : IIncomeRetrieverService
    {
        private readonly IMonoClinet _monoClinet;

        public MonoIncomeService(IMonoClinet monoClinet)
        {
            _monoClinet = monoClinet;
        }

        public async Task<AcountIncomeModel> RetriveAccountIncome(string accountId)
        {
            var result = await _monoClinet.GetAccountIncome(accountId);

            if (result == null) return null;

            var income = result.MonthlyIncome;
            return new AcountIncomeModel()
            {
                MonthlyIncome = income
            };
        }
    }


}
