using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DlmCredit.Application.Disbursement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DlmCredit.Api.Controllers
{
    /// <summary>
    /// General functions for finacials related workflows, such as disbursement,
    /// Account financial details and more
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FinancialsController : ControllerBase
    {
        private readonly ILogger<FinancialsController> _logger;
        private readonly ILoanDisbursementService _disbursementService;

        public FinancialsController(ILogger<FinancialsController> logger, ILoanDisbursementService disbursementService)
        {
            _logger = logger;
            _disbursementService = disbursementService;
        }
      
        /// <summary>
        /// Get customer's income details
        /// </summary>
        /// <param name="accountId">Customer Account Id</param>
        /// <returns>Loan details</returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(LoanDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIncomeDetails([FromRoute] string accountId)
        {
            var result = await _disbursementService.GetIncomeDetails(accountId);
            if (result.MonthlyIncome == 0)
                return BadRequest("unable to get loan details for this User");

            return Ok(result);
        }

        /// <summary>
        /// Disburses qualified loan amount to customers based on customer's monthly income
        /// </summary>
        /// <param name="request">Loan Disbursement Request</param>
        /// <returns>a string message stating whether or not disbursement was successful</returns>
        [HttpPost()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DisburseLoan([FromBody] LoanDisbursementRequest request)
        {
            var result = await _disbursementService.DisbursementLoan(request.AccountId);
            if (result == 0m)
                return BadRequest("unable to calculate disbursement for this User");

            return Ok($"A total amount of {result} has been disbursed to account {request}");
        }
    }
}
