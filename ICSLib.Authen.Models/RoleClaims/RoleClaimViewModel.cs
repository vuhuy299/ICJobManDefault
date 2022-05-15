/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.RoleClaims
{
    public class RoleClaimViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Id Quyền")]
        public int RoleId { get; set; }
        [Display(Name = "Loại")]
        public string ClaimType { get; set; }
        [Display(Name = "Giá trị")]
        public string ClaimValue { get; set; }

        public RoleClaimViewModel() { }

        public RoleClaimViewModel(IdentityRoleClaim<Int32> identityRoleClaim)
        {
            Id = identityRoleClaim.Id;
            RoleId = identityRoleClaim.RoleId;
            ClaimType = identityRoleClaim.ClaimType;
            ClaimValue = identityRoleClaim.ClaimValue;
        }

    }
}
