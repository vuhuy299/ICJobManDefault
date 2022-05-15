/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Roles
{
    public class GetRoleHierarchyRequest
    {
        [Display(Name = "Người dùng")]
        public int UserId { set; get; }
        [Display(Name = "Quyền")]
        public int RoleGroupId { set; get; }
        [Display(Name = "Chức năng cha")]
        public int ParentRoleId { set; get; }
        [Display(Name = "Từ khóa")]
        public string Keyword { set; get; }
        [Display(Name = "Hiển thị")]
        public byte IsShow { set; get; }
        [Display(Name = "Trạng thái")]
        public byte StatusId { set; get; }

        public GetRoleHierarchyRequest() {
            UserId = RoleGroupId = ParentRoleId = 0;
            IsShow = StatusId = 2;
            Keyword = string.Empty;
        }
    }
}
