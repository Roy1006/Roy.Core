using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Roy.Core.AuthHelper;
using Roy.Core.AuthHelper.JWT;
using Roy.Core.AuthHelper.JWT.SecurityDemo.Authentication.JWT.AuthHelper;
using Roy.Core.AuthHelper.OverWrite;
using Roy.Core.IServices;
using Roy.Core.Model.ViewModel;

namespace Roy.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login")]
    //[EnableCors("AllowSameDomain")]
    public class AccountController : Controller
    {
        ISysUserInfoService service;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocServices"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="jwtIssuerOptions"></param>
        /// <param name="cache"></param>
        public AccountController(ISysUserInfoService iocServices,IJwtFactory jwtFactory,IOptions<JwtIssuerOptions> jwtIssuerOptions,IMemoryCache cache)
        {
            this.service = iocServices;
            _jwtFactory = jwtFactory;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
            _cache = cache;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Account")]
        public async Task<IActionResult> LoginAsync(LoginViewModel vModel)
        {
            bool isLogin = service.Login(vModel);

            JWTTokenModel jm = new JWTTokenModel
            {
                UserId = vModel.LoginUserId,
                UserName = vModel.LoginUserId,
                Role = "01"
            };

            //if (!isLogin)
            //{
            //    return Json(false);
            //}
            //return BadRequest(ModelState);

            string refreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(jm);

            _cache.Set(refreshToken, vModel.LoginUserId,TimeSpan.FromMinutes(11));
            var token = await _jwtFactory.GenerateEncodeTokenAsync(jm.UserName, refreshToken, claimsIdentity);

            return new OkObjectResult(token);
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenOptions request)
        {
            string userName;
            if (!_cache.TryGetValue(request.RefreshToken, out userName))
            {
                ModelState.AddModelError("refreshtoken_failure", "Invalid refreshtoken.");
                return BadRequest(ModelState);
            }
            if (!request.UserName.Equals(userName))
            {
                ModelState.AddModelError("refreshtoken_failure", "Invalid userName.");
                return BadRequest(ModelState);
            }

            JWTTokenModel user = new JWTTokenModel();
            string newRefreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);

            _cache.Remove(request.RefreshToken);
            _cache.Set(newRefreshToken, user.UserName, TimeSpan.FromMinutes(11));

            var token = await _jwtFactory.GenerateEncodeTokenAsync(user.UserName, newRefreshToken, claimsIdentity);
            return new OkObjectResult(token);
        }
    }
}