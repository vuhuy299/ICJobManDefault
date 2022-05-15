/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

namespace ICSLib.BaseModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { set; get; }
        public string Message { set; get; }
        public T ResultObj { set; get; }
    }
}
