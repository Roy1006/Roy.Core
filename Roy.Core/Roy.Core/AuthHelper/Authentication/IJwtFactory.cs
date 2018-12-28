using Roy.Core.AuthHelper.OverWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roy.Core.AuthHelper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="refreshToken"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<string> GenerateEncodeTokenAsync(string userName,string refreshToken, ClaimsIdentity identity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ClaimsIdentity GenerateClaimsIdentity(JWTTokenModel user);
    }
}
