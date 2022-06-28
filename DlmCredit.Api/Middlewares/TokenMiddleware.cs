using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DlmCredit.Infrastructure.OAuth;

namespace DlmCredit.Api.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtTokenHandler _tokenProvider;

        public TokenMiddleware(RequestDelegate next, IJwtTokenHandler tokenProvider)
        {
            _next = next;
            _tokenProvider = tokenProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            try
            {
                if (token != null)
                {
                    var userinformation = _tokenProvider.ValidateToken(token);
                    context.Items["User"] = userinformation;
                }
            }
            catch (Exception)
            {
            }

            await _next(context);
        }


    }
}
