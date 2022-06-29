using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DlmCredit.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DlmCredit.Infrastructure.OAuth
{
    internal class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly byte[] _key;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public JwtTokenHandler(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _tokenHandler = new JwtSecurityTokenHandler();
            _key = Encoding.ASCII.GetBytes(configuration["JWT:Key"]);
            _userService = userService;
        }
        public string GenerateToken(string userId, UserInformation userInfo = null)
        {
            Dictionary<string, object> claims = new();
            if (userInfo != null)
            {
                foreach (var prop in userInfo.GetType().GetProperties())
                {
                    claims.Add(prop.Name, prop.GetValue(userInfo, null));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature),
                Claims = claims
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }

        public UserInformation ValidateToken(string token)
        {
            _tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = true,
                    ValidateLifetime = false,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"]
                }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;


            return _userService.GetUserDetails(userId);

        }
    }
}
