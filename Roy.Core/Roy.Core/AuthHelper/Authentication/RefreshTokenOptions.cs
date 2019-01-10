using System.ComponentModel.DataAnnotations;

namespace Roy.Core.Authentication
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
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public string RefreshToken { get; set; }
    }
}
