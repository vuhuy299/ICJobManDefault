/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

namespace ICSLib.BaseModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult()
        {
            IsSuccessed = true;
            Message = "Successful";
        }

        public ApiSuccessResult(T resultObject)
        {
            IsSuccessed = true;
            Message = "Successful";
            ResultObj = resultObject;
        }
    }
}
