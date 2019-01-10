using Roy.Core.Model.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roy.Core.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="refreshToken"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<string> GenerateEncodeToken(string userID,string refreshToken, ClaimsIdentity identity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ClaimsIdentity GenerateClaimsIdentity(UserInfoViewModel user);
    }
}
