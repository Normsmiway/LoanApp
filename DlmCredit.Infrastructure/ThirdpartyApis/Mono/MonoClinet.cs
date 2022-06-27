using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DlmCredit.Infrastructure.ThirdpartyApis
{

    internal class MonoClinet : IMonoClinet
    {
        private readonly HttpClient _client;
        private readonly MonoConfig _config;
        private readonly ILogger<MonoClinet> _logger;
        public MonoClinet(IHttpClientFactory httpClientFactory,
            IOptions<MonoConfig> options, ILogger<MonoClinet> logger)
        {
            _config = options.Value;
            _client = httpClientFactory.CreateClient("MonoClient");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("mono-sec-key", _config.Secret);
            _logger = logger;
        }



        public async Task<IncomeResult> GetAccountIncome(string accountId)
        {
            string endpoint = string.Format(MonoEndpoints.Income_v1, accountId);
            var response = await _client.GetAsync(endpoint).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Call to faild with response {response.StatusCode}");
                return null;
            }

            var incomeResult = await response.Content.ReadFromJsonAsync<IncomeResult>();
            return incomeResult;
        }
    }
}
