using System.Threading.Tasks;

namespace DlmCredit.Application.Contracts
{
    public interface IIncomeRetrieverService
    {
        Task<AcountIncomeModel> RetriveAccountIncome(string accountId);
    }
    public class AcountIncomeModel
    {
        public int MonthlyIncome { get; set; }
    }
}
