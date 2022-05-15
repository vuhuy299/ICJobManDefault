/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Models.Roles;
using ICSLib.BaseModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResult<RoleViewModel>> GetById(int id);
        Task<ApiResult<RoleViewModel>> GetWithClaimsById(int id);
        Task<ApiResult<List<RoleViewModel>>> GetAll();
        Task<ApiResult<List<RoleViewModel>>> GetHỉerachy(GetRoleHierarchyRequest request);
        List<RoleViewModel> BuildHỉerachy(List<RoleViewModel> roles);
        Task<ApiResult<List<RoleViewModel>>> GetUserMenus(GetUserMenuRequest request);
        List<RoleViewModel> BuildUserMenus(List<Role> roles);
        Task<ApiResult<PagedResult<RoleViewModel>>> GetListPaging(GetRolePagingRequest request);
        Task<ApiResult<RoleViewModel>> Create(RoleCreateRequest request);
        Task<ApiResult<RoleViewModel>> Update(RoleEditRequest request);
        Task<ApiResult<int>> Delete(int Guid);
    }
}
