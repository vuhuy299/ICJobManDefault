/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.RoleClaims;
using ICSLib.BaseModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services.Interfaces
{
    public interface IRoleClaimService
    {
        Task<ApiResult<RoleClaimViewModel>> GetById(int id);
        Task<ApiResult<List<RoleClaimViewModel>>> GetAll(int roleId);
        Task<ApiResult<PagedResult<RoleClaimViewModel>>> GetListPaging(GetRoleClaimPagingRequest request);
        Task<ApiResult<RoleClaimViewModel>> Create(RoleClaimCreateRequest request);
        Task<ApiResult<RoleClaimViewModel>> Update(RoleClaimEditRequest request);
        Task<ApiResult<int>> Delete(int id);
    }
}
