/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

namespace ICSLib.BaseModels.Common
{

    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}
