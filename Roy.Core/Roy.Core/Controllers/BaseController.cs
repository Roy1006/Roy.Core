using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Roy.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "Permssion")]
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentUser()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
