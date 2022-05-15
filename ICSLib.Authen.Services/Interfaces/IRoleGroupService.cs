/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.RoleGroups;
using ICSLib.BaseModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services.Interfaces
{
    public interface IRoleGroupService
    {
        Task<ApiResult<RoleGroupViewModel>> GetById(int id);
        Task<ApiResult<RoleGroupViewModel>> GetWithRolesById(int id);
        Task<ApiResult<List<RoleGroupViewModel>>> GetAll();
        Task<ApiResult<PagedResult<RoleGroupViewModel>>> GetListPaging(GetRoleGroupPagingRequest request);
        Task<ApiResult<RoleGroupViewModel>> Create(RoleGroupCreateRequest request);
        Task<ApiResult<RoleGroupViewModel>> Update(RoleGroupEditRequest request);
        Task<ApiResult<int>> Delete(int id);
        Task<ApiResult<string>> AssignRole(RoleGroupAssignRoleRequest request);
    }
}
