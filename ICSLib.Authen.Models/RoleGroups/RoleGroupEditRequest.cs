/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleGroups
{
    public class RoleGroupEditRequest
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

        public RoleGroupEditRequest() { }

        public RoleGroupEditRequest(RoleGroupViewModel roleGroup)
        {
            RoleGroupId = roleGroup.RoleGroupId;
            RoleGroupName = roleGroup.RoleGroupName;
            RoleGroupDesc = roleGroup.RoleGroupDesc;
            SortOrder = roleGroup.SortOrder;
            StatusId = roleGroup.StatusId;
        }
    }
}
