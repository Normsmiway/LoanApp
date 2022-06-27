using System.Threading.Tasks;
using DlmCredit.Application.Contracts;

namespace DlmCredit.Application.Disbursement
{
    internal class LoanDisbursementService : ILoanDisbursementService
    {
        private readonly IIncomeRetrieverService _service;
        public LoanDisbursementService(IIncomeRetrieverService service)
        {
            _service = service;
        }
        public async Task<decimal> GetDisbursementAmount(string accountId)
        {
            var result = await _service.RetriveAccountIncome(accountId);
            if (result == null) return 0m;

            var income = result.MonthlyIncome;

            return CalculateAllowedDisbursement(income);
        }

        //User gets 2x of monthly income as disbursement amount
        private decimal CalculateAllowedDisbursement(int income, int multiplyingFactor = 2)
        {
            return income * multiplyingFactor;
        }
    }
}
