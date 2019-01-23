using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Roy.Core.Authentication;
using Roy.Core.IServices;
using Roy.Core.Model.ViewModel;
using Roy.Common;

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
        ISysUserInfoService _userService;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userServices"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="jwtIssuerOptions"></param>
        /// <param name="cache"></param>
        public AccountController(ISysUserInfoService userServices,IJwtFactory jwtFactory,IOptions<JwtIssuerOptions> jwtIssuerOptions,IMemoryCache cache)
        {
            this._userService = userServices;
            _jwtFactory = jwtFactory;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Account")]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            ReturnObject<string> result = new ReturnObject<string>();
            ServerStatus status = new ServerStatus();

            var userInfo = await _userService.GetUserInfo(vm);

            if (userInfo == null)
            {
                //ModelState.AddModelError("login_failure", "Invalid username or Invalid password !");
                //return BadRequest(ModelState);
                result.Data = "Invalid username or Invalid password !";
                result.Status = ServerStatus.LoginFail;

               return new OkObjectResult(result);
            }

            string refreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(userInfo);

            _cache.Set(refreshToken, vm.LoginUserId, TimeSpan.FromMinutes(11));
            var jwtToken = await _jwtFactory.GenerateEncodeToken(userInfo.UserId, refreshToken, claimsIdentity);

            result.Data = jwtToken;
            result.Status = ServerStatus.Success;

            return new OkObjectResult(result);
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenOptions request)
        {
            if (!_cache.TryGetValue(request.RefreshToken, out string userId))
            {
                ModelState.AddModelError("refreshtoken_failure", "Invalid refreshtoken.");
                return BadRequest(ModelState);
            }
            if (!request.UserId.Equals(userId))
            {
                ModelState.AddModelError("refreshtoken_failure", "Invalid userName.");
                return BadRequest(ModelState);
            }
            LoginViewModel vm = new LoginViewModel { LoginUserId = userId };
            var userInfo =await _userService.GetUserInfo(vm);
            string newRefreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(userInfo);

            _cache.Remove(request.RefreshToken);
            _cache.Set(newRefreshToken, userInfo.UserId, TimeSpan.FromMinutes(11));

            var token = await _jwtFactory.GenerateEncodeToken(userInfo.UserId, newRefreshToken, claimsIdentity);
            return new OkObjectResult(token);
        }

    }
}