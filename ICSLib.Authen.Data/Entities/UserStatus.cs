/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.Collections.Generic;

namespace ICSLib.Authen.Data.Entities
{
    public class UserStatus
    {
        public byte UserStatusId { get; set; }
        public string UserStatusName { get; set; }
        public string UserStatusDesc { get; set; }

        public List<User> AppUsers { set; get; }
    }
}
