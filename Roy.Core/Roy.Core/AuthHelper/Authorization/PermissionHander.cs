using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roy.Core.IServices;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Roy.Core.AuthHelper.JWT
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
            //_userServices = userServices;
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

                context.Succeed(requirement);

                //var functions = _moduleService.Query();
                //var requestUrl = httpContext.Request.Path.Value.ToLower();
                //if (functions != null && functions.Result.Count > 0 && functions.Result.Where(e => e.TargetUrl == requestUrl).ToList().Count > 0)
                //{
                //    context.Succeed(requirement);
                //}
            }

            return Task.CompletedTask;
        }
    }
}
