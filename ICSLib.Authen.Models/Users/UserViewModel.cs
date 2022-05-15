/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using UserStatus = ICSLib.Authen.Data.Enums.UserStatus;

namespace ICSLib.Authen.Models.Users
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Tải khoản")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Giới tính")]
        public int GenderId { set; get; }
        [Display(Name = "Tên giới tính")]
        public string GenderName { set; get; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { set; get; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Ghi chú")]
        public string Comments { get; set; }
        [Display(Name = "OAuthId")]
        public string OAuthId { get; set; }
        [Display(Name = "OAuthName")]
        public string OAuthName { get; set; }
        [Display(Name = "Trạng thái")]
        public byte UserStatusId { set; get; }
        [Display(Name = "Tên trạng thái")]
        public string UserStatusName { set; get; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            FullName = user.FullName;
            Avatar = user.Avatar;
            GenderId = user.GenderId;
            GenderName = GenderId == 1 ? "Nam" : "Nữ";
            DateOfBirth = user.DateOfBirth;
            Address = user.Address;
            Comments = user.Comments;
            OAuthId = user.OAuthId;
            OAuthName = user.OAuthName;
            UserStatusId = user.UserStatusId;
            if (UserStatusId == (byte)UserStatus.InActive)
            {
                UserStatusName = "Chưa kích hoạt";
            }
            else if (UserStatusId == (byte)UserStatus.Active)
            {
                UserStatusName = "Đang hoạt động";
            }
            else if (UserStatusId == (byte)UserStatus.Suspend)
            {
                UserStatusName = "Tạm dừng";
            }
            else if (UserStatusId == (byte)UserStatus.Locked)
            {
                UserStatusName = "Bị chặn";
            }
        }
    }
}
