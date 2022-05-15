/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System;

namespace ICSLib.Authen.Data.Entities
{
    public class UserLog
    {
        public int UserLogId { set; get; }
        public int? UserId { set; get; }
        public string UserName { set; get; }
        public string UserFullName { set; get; }
        public string IPAddress { set; get; }
        public string ActionCode { set; get; }
        public string ActionDesc { set; get; }
        public int TargetObjectId { set; get; }
        public int? GuidTargetObjectId { set; get; }
        public string OldeData { set; get; }
        public string NewData { set; get; }
        public DateTime? CrDateTime { set; get; }
    }
}
