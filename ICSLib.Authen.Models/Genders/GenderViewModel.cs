/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Genders
{
    public class GenderViewModel
    {
        [Display(Name = "Id")]
        public int GenderId { set; get; }
        [Display(Name = "Tên")]
        public string GenderName { set; get; }
        [Display(Name = "Mô tả")]
        public string GenderDesc { set; get; }

        public GenderViewModel() { }

        public GenderViewModel(Gender gender)
        {
            GenderId = gender.GenderId;
            GenderName = gender.GenderName;
            GenderDesc = gender.GenderDesc;
        }
    }
}
