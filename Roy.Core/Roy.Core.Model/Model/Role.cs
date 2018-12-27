using System;

namespace Roy.Core.Model
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        ///角色級別
        /// </summary>
        public string RoleLevel { get; set; }
    }
}
