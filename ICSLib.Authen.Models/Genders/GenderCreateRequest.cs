/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Genders
{
    public class GenderCreateRequest
    {
        [Display(Name = "Tên")]
        public string GenderName { set; get; }
        [Display(Name = "Mô tả")]
        public string GenderDesc { set; get; }
    }
}
