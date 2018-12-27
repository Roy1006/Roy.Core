using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roy.Core.Model
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户信息实体
        /// </summary>
        public User() { }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户部门
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Flag { get; set; }   

    }
}
