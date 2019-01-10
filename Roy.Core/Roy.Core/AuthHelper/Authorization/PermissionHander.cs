using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roy.Core.IServices;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Roy.Core.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionHander : AuthorizationHandler<PermissionRequirement>
    {
        IModuleService _moduleService;
        /// <summary>
        /// 
        /// </summary>
        public PermissionHander(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;
            var isAuthencated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthencated)
            {
                //Guid userId;
                //if (!Guid.TryParse(httpContext.User.Claims.SingleOrDefault(s => s.Type == "id").Value, out userId))
                //{
                //    return Task.CompletedTask;
                //}

                var modules = _moduleService.GetUserModules(httpContext.User.Identity.Name);
                var requestUrl = httpContext.Request.Path.Value.ToLower();
                requestUrl = requestUrl.Substring(requestUrl.IndexOf('/',1), requestUrl.Length - requestUrl.IndexOf('/',1));

                if (modules != null && modules.Result.Count > 0 && modules.Result.Where(e => e.TargetUrl.ToLower() == requestUrl).ToList().Count > 0)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
