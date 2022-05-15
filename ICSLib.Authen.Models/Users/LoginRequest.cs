/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Users
{
    public class LoginRequest
    {
        [Display(Name = "Tài khoản")]
        public string UserName { set; get; }
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }
        [Display(Name = "Ghi nhớ")]
        public bool RememberMe { set; get; }
        [Display(Name = "Trở lại")]
        public string ReturnUrl { set; get; }
    }
}
