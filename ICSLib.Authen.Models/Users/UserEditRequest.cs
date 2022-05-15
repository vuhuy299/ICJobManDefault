/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Users
{
    public class UserEditRequest
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }
        [Display(Name = "Giới tính")]
        public int GenderId { set; get; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { set; get; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Ghi chú")]
        public string Comments { get; set; }
        [Display(Name = "Trạng thái")]
        public byte UserStatusId { set; get; }

        public UserEditRequest() { }

        public UserEditRequest(UserViewModel user)
        {
            Id = user.Id;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            FullName = user.FullName;
            GenderId = user.GenderId;
            DateOfBirth = user.DateOfBirth;
            Address = user.Address;
            Comments = user.Comments;
            UserStatusId = user.UserStatusId;
        }
    }
}
