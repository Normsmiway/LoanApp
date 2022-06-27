using System.Threading.Tasks;

namespace DlmCredit.Application.Disbursement
{
    public interface ILoanDisbursementService
    {
        Task<decimal> GetDisbursementAmount(string accountId);
    }
}
