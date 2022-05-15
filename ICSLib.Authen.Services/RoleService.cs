/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.EF;
using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Models.RoleClaims;
using ICSLib.Authen.Models.Roles;
using ICSLib.Authen.Services.Interfaces;
using ICSLib.BaseModels.Common;
using ICSLib.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services
{
    public class RoleService : IRoleService
    {
        private readonly AuthenDbContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        public RoleService(AuthenDbContext context,
            IConfiguration config,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _config = config;
            _roleManager = roleManager;
        }
        public async Task<ApiResult<RoleViewModel>> GetById(int id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (role == null)
                {
                    return new ApiErrorResult<RoleViewModel>(ConstantHelper.DataNotfound);
                }
                return new ApiSuccessResult<RoleViewModel>(new RoleViewModel(role));
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<RoleViewModel>> GetWithClaimsById(int id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (role == null)
                {
                    return new ApiErrorResult<RoleViewModel>(ConstantHelper.DataNotfound);
                }

                var roleViewModel = new RoleViewModel(role);
                var claims = _context.RoleClaims.Where(m => m.RoleId == id).ToList();
                foreach (var claim in claims)
                {
                    roleViewModel.addClaim(new RoleClaimViewModel(claim));
                }

                return new ApiSuccessResult<RoleViewModel>(roleViewModel);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            try
            {
                var query = _roleManager.Roles.AsNoTracking().AsQueryable();

                var data = await query.Select(x => new RoleViewModel(x))
                    .ToListAsync();
                data = data == null ? new List<RoleViewModel>() : data;
                return new ApiSuccessResult<List<RoleViewModel>>(data);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetHỉerachy(GetRoleHierarchyRequest request)
        {
            try
            {
                var query = _roleManager.Roles.AsNoTracking().AsQueryable();

                if (request.ParentRoleId > 0)
                {
                    query = query.Where(m => m.Id == request.ParentRoleId || m.ParentRoleId == request.ParentRoleId);
                }
                if (request.IsShow != 2)
                {
                    query = query.Where(m => m.IsShow == request.IsShow);
                }
                if (request.StatusId != 2)
                {
                    query = query.Where(m => m.StatusId == request.StatusId);
                }
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(m => m.Name.Contains(request.Keyword) || m.Description.Contains(request.Keyword));
                }

                var data = await query
                    .Select(x => new RoleViewModel(x))
                    .ToListAsync();

                data = data == null ? new List<RoleViewModel>() : data;

                var ret_data = BuildHỉerachy(data);

                if (request.UserId > 0)
                {
                    var u_roles = await (from r in _context.Roles
                                         join u in _context.UserRoles on r.Id equals u.RoleId
                                         where u.UserId == request.UserId
                                         select r.Id).ToListAsync();
                    ret_data.Where(m => u_roles.Contains(m.Id)).ToList().ForEach(m => { m.Selected = true; });
                }
                if (request.RoleGroupId > 0)
                {
                    var g_roles = await (from r in _context.Roles
                                         join g in _context.RoleGroupRoles on r.Id equals g.RoleId
                                         where g.RoleGroupId == request.RoleGroupId
                                         select r.Id).ToListAsync();
                    ret_data.Where(m => g_roles.Contains(m.Id)).ToList().ForEach(m => { m.Selected = true; });
                }

                return new ApiSuccessResult<List<RoleViewModel>>(ret_data);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public List<RoleViewModel> BuildHỉerachy(List<RoleViewModel> roles)
        {
            List<RoleViewModel> trees = new List<RoleViewModel>();
            try
            {
                if (roles == null || roles.Count <= 0) return trees;

                var stack = roles.Where(m => m.LevelId == 1).OrderByDescending(m => m.SortOrder).ToList();

                while (stack.Count > 0)
                {
                    var role = stack[stack.Count - 1];
                    trees.Add(role);
                    stack.RemoveAt(stack.Count - 1);

                    stack.AddRange(roles.Where(m => m.ParentRoleId == role.Id).OrderByDescending(m => m.SortOrder).ToList());
                }

                var leading = ":--";

                foreach (var role in trees)
                {
                    var i = 1;
                    while (i < role.LevelId)
                    {
                        role.Name = leading + role.Name;
                        role.Description = leading + role.Description;
                        i++;
                    }
                }

                return trees;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetUserMenus(GetUserMenuRequest request)
        {
            try
            {
                var roles = await (from r in _context.Roles
                                   join u in _context.UserRoles on r.Id equals u.RoleId
                                   where u.UserId == request.UserId
                                        && r.IsShow == 1
                                        && r.StatusId == 1
                                   select r).ToListAsync();

                var ret_data = BuildUserMenus(roles);

                return new ApiSuccessResult<List<RoleViewModel>>(ret_data);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public List<RoleViewModel> BuildUserMenus(List<Role> roles)
        {
            List<RoleViewModel> trees = new List<RoleViewModel>();
            try
            {
                if (roles == null || roles.Count <= 0) return trees;

                var min_level = roles.Min(m => m.LevelId);

                var stack = roles.Where(m => m.LevelId == min_level)
                    .OrderByDescending(m => m.SortOrder)
                    .ToList();

                while (stack.Count > 0)
                {
                    var role = stack[stack.Count - 1];
                    trees.Add(new RoleViewModel(role));
                    stack.RemoveAt(stack.Count - 1);

                    stack.AddRange(roles.Where(m => m.ParentRoleId == role.Id).OrderByDescending(m => m.SortOrder).ToList());
                }

                return trees;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<PagedResult<RoleViewModel>>> GetListPaging(GetRolePagingRequest request)
        {
            try
            {
                var query = _roleManager.Roles.AsNoTracking();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.Name.Contains(request.Keyword)
                    || x.Description.Contains(request.Keyword));
                }

                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new RoleViewModel(x))
                    .ToListAsync();
                var pageResult = new PagedResult<RoleViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data == null ? new List<RoleViewModel>() : data
                };

                return new ApiSuccessResult<PagedResult<RoleViewModel>>(pageResult);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<RoleViewModel>> Create(RoleCreateRequest request)
        {
            try
            {
                request.Controler = string.IsNullOrEmpty(request.Controler) ? string.Empty : request.Controler;
                request.Action = string.IsNullOrEmpty(request.Action) ? string.Empty : request.Action;
                request.Icon = string.IsNullOrEmpty(request.Icon) ? string.Empty : request.Icon;

                var is_exists = await _roleManager.RoleExistsAsync(request.Name);
                if (is_exists)
                {
                    return new ApiErrorResult<RoleViewModel>(ConstantHelper.RoleExists);
                }

                if (request.ParentRoleId <= 0)
                {
                    request.LevelId = 1;
                }
                else
                {
                    request.LevelId = (byte)((await _context.Roles.FindAsync(request.ParentRoleId)).LevelId + 1);
                }

                var role = new Role()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Controler = request.Controler,
                    Action = request.Action,
                    Icon = request.Icon,
                    SortOrder = request.SortOrder,
                    IsShow = request.IsShow,
                    ParentRoleId = request.ParentRoleId,
                    LevelId = request.LevelId,
                    StatusId = request.StatusId
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _context.RoleGroupRoles.Add(new RoleGroupRole()
                    {
                        RoleGroupId = ConstantHelper.SysAdmin_RoleGroupId,
                        RoleId = role.Id
                    });

                    var user_ids = _context.UserRoleGroups
                        .Where(m => m.RoleGoupId == ConstantHelper.SysAdmin_RoleGroupId)
                        .Select(m => m.UserId)
                        .ToList();

                    user_ids.ForEach(u => {
                        _context.UserRoles.Add(new IdentityUserRole<int>()
                        {
                            UserId = u,
                            RoleId = role.Id
                        });
                    });

                    await _context.SaveChangesAsync();

                    return new ApiSuccessResult<RoleViewModel>(new RoleViewModel(role));
                }
                return new ApiErrorResult<RoleViewModel>(ConstantHelper.UpdateError);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<RoleViewModel>> Update(RoleEditRequest request)
        {
            byte ole_level = 0;
            try
            {
                request.Controler = string.IsNullOrEmpty(request.Controler) ? string.Empty : request.Controler;
                request.Action = string.IsNullOrEmpty(request.Action) ? string.Empty : request.Action;
                request.Icon = string.IsNullOrEmpty(request.Icon) ? string.Empty : request.Icon;

                var is_exists = await _roleManager.Roles
                    .Where(m => m.Name.Equals(request.Name) && m.Id != request.Id)
                    .AnyAsync();
                if (is_exists)
                {
                    return new ApiErrorResult<RoleViewModel>(ConstantHelper.RoleExists);
                }

                if (request.ParentRoleId <= 0)
                {
                    request.LevelId = 1;
                }
                else
                {
                    request.LevelId = (byte)((await _context.Roles.FindAsync(request.ParentRoleId)).LevelId + 1);
                }

                var role = await _roleManager.FindByIdAsync(request.Id.ToString());
                if (role == null)
                {
                    return new ApiErrorResult<RoleViewModel>(ConstantHelper.UpdateNotfound);
                }

                ole_level = role.LevelId;

                role.Name = request.Name;
                role.Description = request.Description;
                role.Controler = request.Controler;
                role.Action = request.Action;
                role.Icon = request.Icon;
                role.SortOrder = request.SortOrder;
                role.IsShow = request.IsShow;
                role.ParentRoleId = request.ParentRoleId;
                role.LevelId = request.LevelId;
                role.StatusId = request.StatusId;

                if (ole_level != role.LevelId)
                {
                    List<int> parents = new List<int>() { role.Id };
                    byte level = (byte)(role.LevelId + 1);
                    while (parents.Count > 0)
                    {
                        var subs = _context.Roles
                            .Where(m => parents.Contains(m.ParentRoleId))
                            .ToList();
                        if (subs != null && subs.Count > 0)
                        {
                            parents = subs.Select(m => m.Id).ToList();
                            subs.ForEach(x => { x.LevelId = level; });
                            level++;
                        }
                        else
                        {
                            parents = new List<int>();
                        }
                    }
                }

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<RoleViewModel>(new RoleViewModel(role));
                }
                else
                {
                    return new ApiErrorResult<RoleViewModel>(ConstantHelper.UpdateError);
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
                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (role == null)
                {
                    return new ApiErrorResult<int>(ConstantHelper.DeleteNotfound);
                }

                _context.RoleGroupRoles
                    .Where(m => m.RoleId == id)
                    .ToList()
                    .ForEach(r => {
                        _context.RoleGroupRoles.Remove(r);
                    });

                _context.UserRoles
                    .Where(m => m.RoleId == id)
                    .ToList()
                    .ForEach(u => {
                        _context.UserRoles.Remove(u);
                    });

                _context.Roles.Remove(role);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<int>(1);
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
    }
}
