using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Roy.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTopten")]
        public IActionResult GetTopten()
        {
           return new OkObjectResult("TEST");
        }
    }
}