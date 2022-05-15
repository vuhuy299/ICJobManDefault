/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.EF;
using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Models.RoleGroups;
using ICSLib.Authen.Models.Roles;
using ICSLib.Authen.Services.Interfaces;
using ICSLib.BaseModels.Common;
using ICSLib.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services
{
    public class RoleGroupService : IRoleGroupService
    {
        private readonly AuthenDbContext _context;
        private readonly IRoleService _roleService;
        public RoleGroupService(AuthenDbContext context,
            IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        public async Task<ApiResult<RoleGroupViewModel>> GetById(int id)
        {
            try
            {
                var roleGroup = await _context.RoleGroups.AsNoTracking().FirstOrDefaultAsync(x => x.RoleGroupId == id);
                if (roleGroup == null)
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.DataNotfound);
                }
                return new ApiSuccessResult<RoleGroupViewModel>(new RoleGroupViewModel(roleGroup));
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<RoleGroupViewModel>> GetWithRolesById(int id)
        {
            try
            {
                var roleGroup = await _context.RoleGroups.AsNoTracking().FirstOrDefaultAsync(x => x.RoleGroupId == id);
                if (roleGroup == null)
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.DataNotfound);
                }

                var query = _context.Roles.AsNoTracking();
                var roles = await query
                    .Select(x => new RoleViewModel(x))
                    .ToListAsync();
                
                roles = _roleService.BuildHỉerachy(roles);

                var g_roles = (from r in _context.Roles
                               join g in _context.RoleGroupRoles on r.Id equals g.RoleId
                               where g.RoleGroupId == id
                               select r.Id).ToList();
                roles.Where(m => g_roles.Contains(m.Id)).ToList().ForEach(m => { m.Selected = true; });

                return new ApiSuccessResult<RoleGroupViewModel>(new RoleGroupViewModel(roleGroup, roles));
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<List<RoleGroupViewModel>>> GetAll()
        {
            try
            {
                var query = _context.RoleGroups.AsNoTracking();

                var data = await query.Select(x => new RoleGroupViewModel(x))
                    .OrderBy(m=>m.SortOrder)
                    .ToListAsync();
                data = data == null ? new List<RoleGroupViewModel>() : data;
                return new ApiSuccessResult<List<RoleGroupViewModel>>(data);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<PagedResult<RoleGroupViewModel>>> GetListPaging(GetRoleGroupPagingRequest request)
        {
            try
            {
                
                var query = _context.RoleGroups.AsNoTracking();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.RoleGroupName.Contains(request.Keyword) || x.RoleGroupDesc.Contains(request.Keyword));
                }

                int totalRow = await query.CountAsync();
                var data = await query.OrderBy(m => m.SortOrder)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new RoleGroupViewModel(x))
                    .ToListAsync();
                var pageResult = new PagedResult<RoleGroupViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data == null ? new List<RoleGroupViewModel>() : data
                };

                return new ApiSuccessResult<PagedResult<RoleGroupViewModel>>(pageResult);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<RoleGroupViewModel>> Create(RoleGroupCreateRequest request)
        {
            try
            {
                var is_exists = await _context.RoleGroups
                    .Where(m => m.RoleGroupName.Equals(request.RoleGroupName))
                    .AnyAsync();
                if (is_exists)
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.RoleGroupExists);
                }
                var roleGroup = new RoleGroup()
                {
                    RoleGroupName = request.RoleGroupName,
                    RoleGroupDesc = request.RoleGroupDesc,
                    SortOrder = request.SortOrder,
                    StatusId = request.StatusId
                };
                _context.RoleGroups.Add(roleGroup);
                await _context.SaveChangesAsync();
                if (roleGroup.RoleGroupId <= 0)
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.UpdateError);
                }
                return new ApiSuccessResult<RoleGroupViewModel>(new RoleGroupViewModel(roleGroup));
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<RoleGroupViewModel>> Update(RoleGroupEditRequest request)
        {
            try
            {
                var is_exists = await _context.RoleGroups
                    .Where(m => m.RoleGroupName.Equals(request.RoleGroupName)
                        && m.RoleGroupId != request.RoleGroupId)
                    .AnyAsync();
                if (is_exists)
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.RoleGroupExists);
                }
                var roleGroup = await _context.RoleGroups.FindAsync(request.RoleGroupId);
                if (roleGroup == null)
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.UpdateNotfound);
                }
                roleGroup.RoleGroupName = request.RoleGroupName;
                roleGroup.RoleGroupDesc = request.RoleGroupDesc;
                roleGroup.SortOrder = request.SortOrder;
                roleGroup.StatusId = request.StatusId;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<RoleGroupViewModel>(new RoleGroupViewModel(roleGroup));
                }
                else
                {
                    return new ApiErrorResult<RoleGroupViewModel>(ConstantHelper.UpdateError);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            try
            {
                var roleGroup = await _context.RoleGroups.FindAsync(id);
                if (roleGroup == null)
                {
                    return new ApiErrorResult<int>(ConstantHelper.DeleteNotfound);
                }
                _context.RoleGroups.Remove(roleGroup);

                _context.RoleGroupRoles
                    .Where(m => m.RoleGroupId == id)
                    .ToList()
                    .ForEach(rg => {
                        _context.RoleGroupRoles.Remove(rg);
                    });

                _context.UserRoleGroups
                    .Where(m => m.RoleGoupId == id)
                    .ToList()
                    .ForEach(ug => {
                        _context.UserRoleGroups.Remove(ug);
                    });

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(result);
                }
                else
                {
                    return new ApiErrorResult<int>(ConstantHelper.UpdateError);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<string>> AssignRole(RoleGroupAssignRoleRequest request)
        {
            try
            {
                var selected_ids = request.Roles
                    .Where(m => m.Selected == true)
                    .Select(m => m.Id)
                    .ToList();

                var curr_ids = _context.RoleGroupRoles
                    .Where(m => m.RoleGroupId == request.RoleGroupId)
                    .Select(m => m.RoleId)
                    .ToList();

                var remove_ids = curr_ids.Where(m => !selected_ids.Contains(m)).ToList();

                var add_ids = selected_ids.Where(m => !curr_ids.Contains(m)).ToList();

                _context.RoleGroupRoles
                    .Where(m => m.RoleGroupId == request.RoleGroupId 
                        && remove_ids.Contains(m.RoleId))
                    .ToList()
                    .ForEach(m => { 
                        _context.RoleGroupRoles.Remove(m); 
                    });

                add_ids.ForEach(m => { 
                    _context.RoleGroupRoles.Add(new RoleGroupRole() { 
                        RoleGroupId = request.RoleGroupId, 
                        RoleId = m
                    });

                    var user_ids = _context.UserRoleGroups
                        .Where(m => m.RoleGoupId == request.RoleGroupId)
                        .Select(m => m.UserId)
                        .ToList();

                    user_ids.ForEach(u => {
                        if(_context.UserRoles.Find(u, m) == null)
                        {
                            _context.UserRoles.Add(new IdentityUserRole<int>()
                            {
                                UserId = u,
                                RoleId = m
                            });
                        }
                    });
                });

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<string>(ConstantHelper.UpdateSuccess);
                }
                else
                {
                    return new ApiErrorResult<string>(ConstantHelper.UpdateError);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }
    }
}
