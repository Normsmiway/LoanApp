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
        public async Task<decimal> DisbursementLoan(string accountId)
        {
            var result = await _service.RetriveAccountIncome(accountId);
            if (result == null) return 0m;

            var income = result.MonthlyIncome;

            return CalculateAllowedDisbursement(income);
        }

        public async Task<LoanDetails> GetIncomeDetails(string accountId)
        {
            var result = await _service.RetriveAccountIncome(accountId);
            if (result == null) return new LoanDetails() { AccountId = accountId, MonthlyIncome = 0 };

            return new LoanDetails() { AccountId = accountId, MonthlyIncome = result.MonthlyIncome };
        }

        //User gets 2x of monthly income as disbursement amount
        private decimal CalculateAllowedDisbursement(int income, int multiplyingFactor = 2)
        {
            return income * multiplyingFactor;
        }
    }
}
