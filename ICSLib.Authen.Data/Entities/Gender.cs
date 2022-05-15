/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.Collections.Generic;

namespace ICSLib.Authen.Data.Entities
{
    public class Gender
    {
        public int GenderId { set; get; }
        public string GenderName { set; get; }
        public string GenderDesc { set; get; }

        public List<User> AppUsers { set; get; }
    }
}
