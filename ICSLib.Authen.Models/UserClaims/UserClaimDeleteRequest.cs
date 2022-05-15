/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.UserClaims
{
    public class UserClaimDeleteRequest
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Id Người dùng")]
        public int UserId { get; set; }
        [Display(Name = "Loại")]
        public string ClaimType { get; set; }
        [Display(Name = "Giá trị")]
        public string ClaimValue { get; set; }
    }
}
