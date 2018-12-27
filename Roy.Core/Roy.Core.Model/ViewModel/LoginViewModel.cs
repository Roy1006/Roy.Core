using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roy.Core.Model.ViewModel
{
    /// <summary>
    /// 登陆页面的Model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 登陆用户名
        /// </summary>
        [Required(ErrorMessage = "必须填写用户名！")]
        public string LoginUserId { get; set; }

        /// <summary>
        ///登陆密码
        /// </summary>
        [Required(ErrorMessage = "必须输入密码！")]
        public string LoginPwd { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }

        /// <summary>
        /// 是否记住
        /// </summary>
        public string IsMember { get; set; }
    }
}
