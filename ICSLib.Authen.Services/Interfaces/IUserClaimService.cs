/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.UserClaims;
using ICSLib.BaseModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services.Interfaces
{
    public interface IUserClaimService
    {
        Task<ApiResult<UserClaimViewModel>> GetById(int id);
        Task<ApiResult<List<UserClaimViewModel>>> GetAll(int userId);
        Task<ApiResult<PagedResult<UserClaimViewModel>>> GetListPaging(GetUserClaimPagingRequest request);
        Task<ApiResult<UserClaimViewModel>> Create(UserClaimCreateRequest request);
        Task<ApiResult<UserClaimViewModel>> Update(UserClaimEditRequest request);
        Task<ApiResult<int>> Delete(int id);
    }
}
