/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleGroups
{
    public class RoleGroupAssignRoleRequest
    {
        [Display(Name = "Id Quyền")]
        public int RoleGroupId { get; set; }
        [Display(Name = "Quyền hệ thống")]
        public string RoleGroupName { get; set; }
        [Display(Name = "Chức năng")]
        public List<RoleViewModel> Roles { get; set; }
}
}
