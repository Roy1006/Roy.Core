using System;
using System.Collections.Generic;
using System.Text;

namespace Roy.Core.Model
{
    /// <summary>
    /// 部门实体
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 部门父级ID
        /// </summary>
        public string UpDeptId { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public string DeptType { get; set; }

        /// <summary>
        /// 部门业务机构号
        /// </summary>
        public string BrNo { get; set; }

        /// <summary>
        /// 部门用户机构号
        /// </summary>
        public string EhrNo { get; set; }
    }
}
