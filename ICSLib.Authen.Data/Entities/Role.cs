/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.AspNetCore.Identity;
using System;

namespace ICSLib.Authen.Data.Entities
{
    public class Role : IdentityRole<Int32>
    {
        public string Description { set; get; }
        public string Controler { set; get; }
        public string Action { set; get; }
        public string Icon { set; get; }
        public short SortOrder { set; get; }
        public byte IsShow { set; get; }
        public int ParentRoleId { set; get; }
        public byte LevelId { set; get; }
        public byte StatusId { set; get; }
    }
}
