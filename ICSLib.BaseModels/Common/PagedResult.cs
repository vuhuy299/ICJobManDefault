/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System.Collections.Generic;

namespace ICSLib.BaseModels.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { set; get; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
