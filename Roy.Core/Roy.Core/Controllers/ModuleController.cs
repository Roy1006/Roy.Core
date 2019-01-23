using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roy.Core.IServices;
using Roy.Core.Model;
using Roy.Core.Model.ViewModel;

namespace Roy.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Module")]
    [Authorize(Policy = "Client")]    
    public class ModuleController : Controller
    {
        IModuleService service;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fService"></param>
        public ModuleController(IModuleService fService)
        {
            this.service = fService;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetModules")]
        public async Task<List<ModuleViewModel>> GetModules()
        {           
            string userId = HttpContext.User.Identity.Name;
            return await service.GetUserModules(userId);
        }
    }
}