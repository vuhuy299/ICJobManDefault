/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ICSLib.Authen.Services.Interfaces;
using ICSLib.BaseModels.Common;
using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Data.Enums;
using ICSLib.Authen.Models.Users;
using ICSLib.Authen.Models.Roles;
using ICSLib.Authen.Data.EF;
using ICSLib.Authen.Models.RoleGroups;
using Newtonsoft.Json;

namespace ICSLib.Authen.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly IRoleService _roleService;
        private readonly AuthenDbContext _context;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IConfiguration config,
            IRoleService roleService,
            AuthenDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _roleService = roleService;
            _context = context;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return new ApiErrorResult<string>(ConstantHelper.AccountNotfound);
                }
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);
                if (!result.Succeeded)
                {
                    return new ApiErrorResult<string>(ConstantHelper.PasswordWrong);
                }

                var roles = _userManager.GetRolesAsync(user);
                var role_trees = (await _roleService.GetUserMenus(new GetUserMenuRequest() { 
                    UserId = user.Id
                })).ResultObj;

                role_trees = role_trees == null ? new List<RoleViewModel>() : role_trees;

                var claims = new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FullName),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(role_trees)),
                    new Claim(ClaimTypes.Role, string.Join(";", roles))
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds
                    );

                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return new ApiSuccessResult<string>(tokenStr);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<string>> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new ApiSuccessResult<string>("Signed out");
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<UserViewModel>> Register(RegisterRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user != null)
                {
                    return new ApiErrorResult<UserViewModel>(ConstantHelper.UserNameExists);
                }
                if (!string.IsNullOrEmpty(request.Email))
                {
                    user = await _userManager.FindByEmailAsync(request.Email);
                    if (user != null)
                    {
                        return new ApiErrorResult<UserViewModel>(ConstantHelper.EmailExists);
                    }
                }

                user = new User()
                {
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    FullName = request.FullName,
                    Avatar = string.Empty,
                    GenderId = request.GenderId,
                    DateOfBirth = request.DateOfBirth,
                    Address = string.Empty,
                    Comments = string.Empty,
                    OAuthId = string.Empty,
                    OAuthName = string.Empty,
                    CrDateTime = DateTime.Now,
                    ActiveDateTime = DateTime.Now,
                    UserStatusId = (byte)Data.Enums.UserStatus.Active
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<UserViewModel>(new UserViewModel(user));
                }

                if (result.Errors != null)
                {
                    string errors = string.Empty;
                    foreach (var err in result.Errors)
                    {
                        if (!string.IsNullOrEmpty(errors)) errors += "; ";
                        errors += err.Description;
                    }
                    return new ApiErrorResult<UserViewModel>(errors);
                }
                return new ApiErrorResult<UserViewModel>(ConstantHelper.UpdateError);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<UserViewModel>> Create(UserCreateRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user != null)
                {
                    return new ApiErrorResult<UserViewModel>(ConstantHelper.UserNameExists);
                }
                if (!string.IsNullOrEmpty(request.Email))
                {
                    user = await _userManager.FindByEmailAsync(request.Email);
                    if (user != null)
                    {
                        return new ApiErrorResult<UserViewModel>(ConstantHelper.EmailExists);
                    }
                }

                user = new User()
                {
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    FullName = request.FullName,
                    Avatar = string.Empty,
                    GenderId = request.GenderId,
                    DateOfBirth = request.DateOfBirth,
                    Address = string.Empty,
                    Comments = string.Empty,
                    OAuthId = string.Empty,
                    OAuthName = string.Empty,
                    CrDateTime = DateTime.Now,
                    ActiveDateTime = DateTime.Now,
                    UserStatusId = (byte)Data.Enums.UserStatus.Active
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<UserViewModel>(new UserViewModel(user));
                }

                if (result.Errors != null)
                {
                    string errors = string.Empty;
                    foreach (var err in result.Errors)
                    {
                        if (!string.IsNullOrEmpty(errors)) errors += "; ";
                        errors += err.Description;
                    }
                    return new ApiErrorResult<UserViewModel>(errors);
                }
                return new ApiErrorResult<UserViewModel>(ConstantHelper.UpdateError);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<UserViewModel>> Update(UserEditRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Email))
                {
                    var userExists = _userManager.Users
                                    .FirstOrDefault(x => x.Id != request.Id && x.Email == request.Email);
                    if (userExists != null)
                    {
                        return new ApiErrorResult<UserViewModel>(ConstantHelper.EmailExists);
                    }
                }
                var user = await _userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<UserViewModel>(ConstantHelper.UpdateNotfound);
                }

                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;
                user.FullName = request.FullName;
                user.GenderId = request.GenderId;
                user.DateOfBirth = request.DateOfBirth;
                user.Address = request.Address;
                user.Comments = request.Comments;
                user.UserStatusId = request.UserStatusId;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new ApiSuccessResult<UserViewModel>(new UserViewModel(user));
                }

                if (result.Errors != null)
                {
                    string errors = string.Empty;
                    foreach (var err in result.Errors)
                    {
                        if (!string.IsNullOrEmpty(errors)) errors += "; ";
                        errors += err.Description;
                    }
                    return new ApiErrorResult<UserViewModel>(errors);
                }
                return new ApiErrorResult<UserViewModel>(ConstantHelper.UpdateError);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<bool>> Delete(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>(ConstantHelper.DeleteNotfound);
                }

                _context.UserRoleGroups
                    .Where(m => m.UserId == userId)
                    .ToList()
                    .ForEach(g => {
                        _context.UserRoleGroups.Remove(g);
                    });

                _context.UserRoles
                    .Where(m => m.UserId == userId)
                    .ToList()
                    .ForEach(u => {
                        _context.UserRoles.Remove(u);
                    });

                _context.Users.Remove(user);

                //var result = await _userManager.DeleteAsync(user);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }

                /*if (result.Errors != null)
                {
                    string errors = string.Empty;
                    foreach (var err in result.Errors)
                    {
                        if (!string.IsNullOrEmpty(errors)) errors += "; ";
                        errors += err.Description;
                    }
                    return new ApiErrorResult<bool>(errors);
                }*/
                return new ApiErrorResult<bool>(ConstantHelper.UpdateError);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<UserViewModel>> GetById(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>(ConstantHelper.DataNotfound);
            }
            return new ApiSuccessResult<UserViewModel>(new UserViewModel(user));
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetListPaging(GetUserPagingRequest request)
        {
            try
            {
                var query = _userManager.Users.AsNoTracking();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    string keyword = request.Keyword.ToLower();
                    query = query.Where(x => x.FullName.ToLower().Contains(keyword)
                                || x.FirstName.ToLower().Contains(keyword)
                                || x.LastName.ToLower().Contains(keyword)
                                || x.UserName.ToLower().Contains(keyword)
                                || x.Email.ToLower().Contains(keyword)
                                || x.PhoneNumber.ToLower().Contains(keyword));
                }

                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new UserViewModel(x))
                    .ToListAsync();
                var pageResult = new PagedResult<UserViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data == null ? new List<UserViewModel>() : data
                };

                return new ApiSuccessResult<PagedResult<UserViewModel>>(pageResult);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<UserAssignRoleRequest>> GetUserRoles(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserAssignRoleRequest>(ConstantHelper.DataNotfound);
            }

            var roles = await _context.Roles
                .Select(m => new RoleViewModel(m))
                .ToListAsync();
            var userRoles = await _context.UserRoles
                .Where(m=>m.UserId==userId)
                .Select(m=>m.RoleId)
                .ToListAsync();

            roles.Where(m => userRoles.Contains(m.Id))
                .ToList()
                .ForEach(m => { m.Selected = true; });

            roles = _roleService.BuildHỉerachy(roles);

            var resultObj = new UserAssignRoleRequest()
            {
                UserId = user.Id,
                Fullname = user.FullName,
                Roles = roles
            };
            return new ApiSuccessResult<UserAssignRoleRequest>(resultObj);
        }

        public async Task<ApiResult<bool>> AssignRole(UserAssignRoleRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return new ApiErrorResult<bool>(ConstantHelper.AccountNotfound);
                }

                var removeRoles = request.Roles
                    .Where(x => x.Selected == false)
                    .Select(x => x.Id)
                    .ToList();

                removeRoles.ForEach(m => {
                    _context.UserRoles.Remove(new IdentityUserRole<int>() { 
                        UserId = request.UserId, 
                        RoleId = m
                    });
                });

                var addRoles = request.Roles
                    .Where(x => x.Selected == true)
                    .Select(x => x.Id)
                    .ToList();

                addRoles.ForEach(m => {
                    if (_context.UserRoles.Find(request.UserId, m) == null) {
                        _context.UserRoles.Add(new IdentityUserRole<int>() { 
                            UserId = request.UserId,
                            RoleId = m
                        });
                    }
                });

                var result = await _context.SaveChangesAsync();

                if (result > 0) {
                    return new ApiSuccessResult<bool>(true);
                }

                return new ApiErrorResult<bool>(ConstantHelper.UpdateError);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), new System.Diagnostics.StackTrace().GetFrames()[0].GetMethod().Name);
                throw;
            }
        }

        public async Task<ApiResult<UserAssignRoleGroupRequest>> GetUserRoleGroups(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserAssignRoleGroupRequest>(ConstantHelper.DataNotfound);
            }

            var roleGroups = await _context.RoleGroups
                .OrderBy(m => m.SortOrder)
                .Select(m => new RoleGroupViewModel(m))
                .ToListAsync();
            var userRoleGroups = await _context.UserRoleGroups
                .Where(m => m.UserId == userId)
                .Select(m => m.RoleGoupId)
                .ToListAsync();

            roleGroups.Where(m => userRoleGroups.Contains(m.RoleGroupId))
                .ToList()
                .ForEach(m => { m.Selected = true; });

            var resultObj = new UserAssignRoleGroupRequest()
            {
                UserId = user.Id,
                Fullname = user.FullName,
                RoleGroups = roleGroups
            };
            return new ApiSuccessResult<UserAssignRoleGroupRequest>(resultObj);
        }

        public async Task<ApiResult<bool>> AssignRoleGroup(UserAssignRoleGroupRequest request)
        {
            try
            {
                var selected_ids = request.RoleGroups
                    .Where(m => m.Selected == true)
                    .Select(m => m.RoleGroupId)
                    .ToList();

                var hold_roles = (from r in _context.Roles
                                  join g in _context.RoleGroupRoles on r.Id equals g.RoleId
                                  where selected_ids.Contains(g.RoleGroupId)
                                  select r.Id).ToList();

                var curr_ids = _context.UserRoleGroups
                    .Where(m => m.UserId == request.UserId)
                    .Select(m => m.RoleGoupId)
                    .ToList();

                var remove_ids = curr_ids.Where(m => !selected_ids.Contains(m)).ToList();

                var add_ids = selected_ids.Where(m => !curr_ids.Contains(m)).ToList();

                var remove_groups = _context.UserRoleGroups
                    .Where(m => m.UserId == request.UserId && remove_ids.Contains(m.RoleGoupId))
                    .ToList();

                remove_groups.ForEach(m => { 
                    _context.UserRoleGroups.Remove(m);

                    var g_roles = (from r in _context.Roles
                                   join g in _context.RoleGroupRoles on r.Id equals g.RoleId
                                   where g.RoleGroupId == m.RoleGoupId
                                   select r.Id).ToList();

                    g_roles.Where(m => !hold_roles.Contains(m))
                        .ToList()
                        .ForEach(m => {
                            _context.UserRoles.Remove(new IdentityUserRole<int>() { 
                                UserId = request.UserId,
                                RoleId = m
                            });
                        });
                });

                add_ids.ForEach(m => { 
                    _context.UserRoleGroups.Add(new UserRoleGroup() { 
                        UserId = request.UserId, 
                        RoleGoupId = m 
                    });

                    var g_roles = (from r in _context.Roles
                                   join g in _context.RoleGroupRoles on r.Id equals g.RoleId
                                   where g.RoleGroupId == m
                                   select r.Id).ToList();

                    foreach (var r in g_roles)
                    {
                        if (_context.UserRoles.Find(request.UserId, r) == null) {
                            _context.UserRoles.Add(new IdentityUserRole<int>() { 
                                UserId = request.UserId, 
                                RoleId = r 
                            });
                        }
                    }
                });

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else
                {
                    return new ApiErrorResult<bool>(ConstantHelper.UpdateError);
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
