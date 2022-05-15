/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Users;
using ICSLib.BaseModels.Common;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<string>> SignOut();
        Task<ApiResult<UserViewModel>> Register(RegisterRequest request);
        Task<ApiResult<UserViewModel>> Create(UserCreateRequest request);
        Task<ApiResult<UserViewModel>> Update(UserEditRequest request);
        Task<ApiResult<bool>> Delete(int userId);
        Task<ApiResult<UserViewModel>> GetById(int userId);
        Task<ApiResult<PagedResult<UserViewModel>>> GetListPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> AssignRole(UserAssignRoleRequest request);
        Task<ApiResult<UserAssignRoleRequest>> GetUserRoles(int userId);
        Task<ApiResult<UserAssignRoleGroupRequest>> GetUserRoleGroups(int userId);
        Task<ApiResult<bool>> AssignRoleGroup(UserAssignRoleGroupRequest request);
    }
}
