using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Roy.Core.Model;
using Roy.Core.Model.ViewModel;

namespace Roy.Core.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptions"></param>
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ClaimsIdentity GenerateClaimsIdentity(UserInfoViewModel user)
        {
            var claimIdentity = new ClaimsIdentity(new GenericIdentity(user.UserId, "Token"));
            //claimIdentity.AddClaim(new Claim("id", user.UserId));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserId));
            //如果多角色，foreach循环添加
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, user.RoleId));

            return claimIdentity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<string> GenerateEncodeToken(string userId,string refreshToken, ClaimsIdentity identity)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(ClaimTypes.Name),
                identity.FindFirst("id")
            };
            claims.AddRange(identity.FindAll(ClaimTypes.Role));

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
                );
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                auth_token = encodeJwt,
                refresh_token = refreshToken,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                token_type = "Bearer"
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private static long ToUnixEpochDate(DateTime date)
         => (long)Math.Round((date.ToUniversalTime() -
                              new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                             .TotalSeconds);
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
