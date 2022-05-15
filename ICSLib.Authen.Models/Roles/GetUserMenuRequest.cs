/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Roles
{
    public class GetUserMenuRequest
    {
        [Display(Name = "Người dùng")]
        public int UserId { set; get; }
    }
}
