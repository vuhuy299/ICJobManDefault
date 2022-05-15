/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.UserClaims
{
    public class UserClaimViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Id Người dùng")]
        public int UserId { get; set; }
        [Display(Name = "Loại")]
        public string ClaimType { get; set; }
        [Display(Name = "Giá trị")]
        public string ClaimValue { get; set; }

        public UserClaimViewModel() { }

        public UserClaimViewModel(IdentityUserClaim<Int32> identityUserClaim)
        {
            Id = identityUserClaim.Id;
            UserId = identityUserClaim.UserId;
            ClaimType = identityUserClaim.ClaimType;
            ClaimValue = identityUserClaim.ClaimValue;
        }
    }
}
