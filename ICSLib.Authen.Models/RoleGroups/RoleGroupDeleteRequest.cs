/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleGroups
{
    public class RoleGroupDeleteRequest
    {
        [Display(Name = "Id")]
        public int RoleGroupId { get; set; }
        [Display(Name = "Tên")]
        public string RoleGroupName { get; set; }
        [Display(Name = "Mô tả")]
        public string RoleGroupDesc { get; set; }
    }
}
