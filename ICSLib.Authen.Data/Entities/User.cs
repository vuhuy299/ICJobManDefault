/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.AspNetCore.Identity;
using System;

namespace ICSLib.Authen.Data.Entities
{
    public class User : IdentityUser<Int32>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public int GenderId { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string Address { get; set; }
        public string Comments { get; set; }
        public string OAuthId { get; set; }
        public string OAuthName { get; set; }
        public DateTime CrDateTime { set; get; }
        public DateTime? ActiveDateTime { set; get; }
        public byte UserStatusId { set; get; }

        public Gender Gender { set; get; }
        public UserStatus UserStatus { set; get; }
    }
}
