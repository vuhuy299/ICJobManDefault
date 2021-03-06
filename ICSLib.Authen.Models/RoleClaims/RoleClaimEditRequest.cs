/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleClaims
{
    public class RoleClaimEditRequest
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Id Quyền")]
        public int RoleId { get; set; }
        [Display(Name = "Loại")]
        public string ClaimType { get; set; }
        [Display(Name = "Giá trị")]
        public string ClaimValue { get; set; }

        public RoleClaimEditRequest() { }

        public RoleClaimEditRequest(RoleClaimViewModel roleClaimViewModel)
        {
            Id = roleClaimViewModel.Id;
            RoleId = roleClaimViewModel.RoleId;
            ClaimType = roleClaimViewModel.ClaimType;
            ClaimValue = roleClaimViewModel.ClaimValue;
        }
    }
}
