using System.Threading.Tasks;

namespace DlmCredit.Infrastructure.ThirdpartyApis
{
    public interface IMonoClinet
    {
        public Task<IncomeResult> GetAccountIncome(string accountId);
    }
}
