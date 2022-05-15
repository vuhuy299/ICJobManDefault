/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tài khoản")]
        public string UserName { set; get; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }
        [Display(Name = "Giới tính")]
        public int GenderId { set; get; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { set; get; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "OAuthId")]
        public string OAuthId { get; set; }
        [Display(Name = "OAuthName")]
        public string OAuthName { get; set; }
    }
}
