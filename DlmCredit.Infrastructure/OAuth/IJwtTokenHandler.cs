using DlmCredit.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace DlmCredit.Infrastructure.OAuth
{
    public interface IJwtTokenHandler
    {
        public string GenerateToken(string userId, UserInformation userInfo = null);
        public UserInformation ValidateToken(string token);
    }
}
