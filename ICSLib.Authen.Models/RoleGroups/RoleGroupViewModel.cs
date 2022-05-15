/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Models.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleGroups
{
    public class RoleGroupViewModel
    {
        [Display(Name = "Id")]
        public int RoleGroupId { get; set; }
        [Display(Name = "Tên")]
        public string RoleGroupName { get; set; }
        [Display(Name = "Mô tả")]
        public string RoleGroupDesc { get; set; }
        [Display(Name = "Thứ tự")]
        public int SortOrder { get; set; }
        [Display(Name = "Trạng thái")]
        public byte StatusId { get; set; }

        [Display(Name = "Chọn")]
        public bool Selected { set; get; }

        [Display(Name = "Chức năng")]
        public List<RoleViewModel> Roles { set; get; }

        public RoleGroupViewModel() { }

        public RoleGroupViewModel(RoleGroup roleGroup) {
            RoleGroupId = roleGroup.RoleGroupId;
            RoleGroupName = roleGroup.RoleGroupName;
            RoleGroupDesc = roleGroup.RoleGroupDesc;
            SortOrder = roleGroup.SortOrder;
            StatusId = roleGroup.StatusId;

            Roles = new List<RoleViewModel>();
        }

        public RoleGroupViewModel(RoleGroup roleGroup, List<RoleViewModel> roles)
        {
            RoleGroupId = roleGroup.RoleGroupId;
            RoleGroupName = roleGroup.RoleGroupName;
            RoleGroupDesc = roleGroup.RoleGroupDesc;
            SortOrder = roleGroup.SortOrder;
            StatusId = roleGroup.StatusId;

            Roles = roles == null ? new List<RoleViewModel>() : roles;
        }
    }
}
