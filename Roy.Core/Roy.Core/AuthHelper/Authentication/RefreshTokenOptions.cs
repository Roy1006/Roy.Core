using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roy.Core.AuthHelper.JWT
{
    /// <summary>
    /// 
    /// </summary>
    public class RefreshTokenOptions
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public string RefreshToken { get; set; }
    }
}
