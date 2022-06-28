using System.Threading.Tasks;

namespace DlmCredit.Application.Disbursement
{
    public interface ILoanDisbursementService
    {
        Task<LoanDetails> GetIncomeDetails(string accountId);
        Task<decimal> DisbursementLoan(string accountId);
    }


    public class LoanDetails
    {
        public string AccountId { get; set; }
        public int MonthlyIncome { get; set; }
    }
}
