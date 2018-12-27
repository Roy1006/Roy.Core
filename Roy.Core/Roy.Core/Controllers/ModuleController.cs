using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roy.Core.IServices;
using Roy.Core.Model;

namespace Roy.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Module")]
    [Authorize(Policy = "Permssion")]
    //[EnableCors("AllowDomain")]
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
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleModule")]
        public async Task<List<Module>> GetSingleModule(string moduleId)
        {
            return await service.Query();
        }
    }
}