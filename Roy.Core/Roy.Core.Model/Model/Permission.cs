using System;
using System.Collections.Generic;
using System.Text;

namespace Roy.Core.Model
{
    /// <summary>
    /// 角色，菜单权限表
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单或者页面元素ID
        /// </summary>
        public string RolepElement { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsUse { get; set; }

        /// <summary>
        /// 控件类型：菜单，按钮，页卡....
        /// </summary>
        public string RolepType { get; set; }
    }
}
