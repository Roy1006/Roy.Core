using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Roy.Core.Middware.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidataModelFilter : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                XcHttpResult result = new XcHttpResult() { Result = false };

                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        result.Msg += error.ErrorMessage + "|";
                    }
                }

                context.Result = new JsonResult(result);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class XcHttpResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Result { get; set; }
    }
}
