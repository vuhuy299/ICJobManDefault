/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Users
{
    public class UserAssignRoleRequest
    {
        [Display(Name = "Tài khoản")]
        public int UserId { set; get; }
        [Display(Name = "Họ và Tên")]
        public string Fullname { set; get; }
        [Display(Name = "Quyền")]
        public List<RoleViewModel> Roles { set; get; }
    }
}
