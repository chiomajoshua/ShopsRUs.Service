using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShopsRUs.Service.Core.Config;
using ShopsRUs.Service.Core.Storage.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ShopsRUs.Service.Features.Security.Services
{
    public interface ITokenService
    {
        string CreateToken(ShopRusUser shopRusUser);
    }
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly UserManager<ShopRusUser> _userManager;
        private readonly IOptions<JwtToken> _tokenInformation;
        public TokenService(ILogger<TokenService> logger, UserManager<ShopRusUser> userManager, IOptions<JwtToken> tokenInformation)
        {
            _userManager = userManager;
            _logger = logger;
            _tokenInformation = tokenInformation;
        }
        public string CreateToken(ShopRusUser shopRusUser)
        {
            try
            {
                var userRoles = _userManager.GetRolesAsync(shopRusUser).Result;
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, shopRusUser.UserName),
                new Claim(ClaimTypes.Email, shopRusUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                userRoles.ToList().ForEach(userRole => claims.Add(new Claim(ClaimTypes.Role, userRole)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenInformation.Value.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = new DateTimeOffset(DateTime.Now).DateTime,
                    Issuer = _tokenInformation.Value.Issuer,
                    Audience = _tokenInformation.Value.Audience,
                    Expires = DateTime.Now.AddHours(Convert.ToInt32(_tokenInformation.Value.Expiry)),
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception exception)
            {
                _logger.LogError($"CreateToken: Error Occured {JsonConvert.SerializeObject(exception)}");
                return string.Empty;
            }
        }
    }
}
