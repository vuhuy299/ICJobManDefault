/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Users
{
    public class UserDeleteRequest
    {
        [Display(Name = "Id")]
        public int Id { set; get; }
        [Display(Name = "Tải khoản")]
        public string UserName { set; get; }
        [Display(Name = "Họ và Tên")]
        public string FullName { set; get; }
    }
}
