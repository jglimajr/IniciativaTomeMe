using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InteliSystem.Utils.Enumerators;
using Microsoft.IdentityModel.Tokens;

namespace InteliSystem.Utils.Functions
{
    public static class JwtValidate
    {


        public static string JwsToken { get => @"Ed8be4d72eac44ba8e43c75bc4b4b93B"; }

        public static string GenerateJWSToken(string identifier, int expireminutes = 30, RulesValues rules = RulesValues.Customer)
        {
            byte[] key = Convert.FromBase64String(JwsToken);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, identifier),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(RulesValues), rules)),
                }),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddYears(expireminutes)

            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        public static bool IsNotValidateToken(string identifier, string token)
        {
            string valuename = null;
            ClaimsPrincipal principal = GetClaims(token);
            if (principal == null)
                return false;

            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch
            {
                return false;
            }
            Claim CostumernameClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            valuename = CostumernameClaim.Value;
            return (valuename.Equals(identifier));
        }

        private static ClaimsPrincipal GetClaims(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;

                byte[] key = Convert.FromBase64String(JwsToken);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}