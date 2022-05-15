/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

namespace ICSLib.Authen.Data.Entities
{
    public class RoleGroup
    {
        public int RoleGroupId { get; set; }
        public string RoleGroupName { get; set; }
        public string RoleGroupDesc { get; set; }
        public int SortOrder { get; set; }
        public byte StatusId { get; set; }
    }
}
