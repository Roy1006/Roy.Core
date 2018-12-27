using System;
using System.Collections.Generic;
using System.Text;

namespace Roy.Core.Model
{
    /// <summary>
    /// 菜单，功能表
    /// </summary>
    public class Module
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 菜单父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 菜单的URL
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 菜单的排序
        /// </summary>
        public string SortFlag { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public string IsMenu { get; set; }
    }
}
