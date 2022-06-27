using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DlmCredit.Application.Disbursement;

namespace DlmCredit.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialsController : ControllerBase
    {
        private readonly ILogger<FinancialsController> _logger;
        private readonly ILoanDisbursementService _disbursementService;

        public FinancialsController(ILogger<FinancialsController> logger, ILoanDisbursementService disbursementService)
        {
            _logger = logger;
            _disbursementService = disbursementService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get([FromRoute] string accountId)
        {
            var result = await _disbursementService.GetDisbursementAmount(accountId);
            if (result == 0m)
                return BadRequest("unable to calculate disbursement for this User");

            return Ok(result);
        }
    }

   
   
}
