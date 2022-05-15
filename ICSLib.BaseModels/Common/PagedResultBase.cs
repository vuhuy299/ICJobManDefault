/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System;

namespace ICSLib.BaseModels.Common
{
    public class PagedResultBase
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalRecords { set; get; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
