/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.BaseModels.Common;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleClaims
{
    public class GetRoleClaimPagingRequest : PagingRequestBase
    {
        [Display(Name = "Quyền")]
        public int RoleId { set; get; }
        [Display(Name = "Từ khóa")]
        public string Keyword { set; get; }
    }
}
