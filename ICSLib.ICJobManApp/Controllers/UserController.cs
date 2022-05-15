/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Users;
using ICSLib.Authen.Services.Interfaces;
using ICSLib.BaseModels.Common;
using ICSLib.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ICSLib.JobManApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(
            IConfiguration configuration,
            IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [Authorize(Roles = "User-Index")]
        [HttpGet("user/index")]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.Keyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword;
                var request = new GetUserPagingRequest()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Keyword = keyword
                };

                var result = await _userService.GetListPaging(request);

                if (!result.IsSuccessed) {
                    ModelState.AddModelError(string.Empty, (string.IsNullOrEmpty(result.Message) ? ConstantHelper.LoadingError : result.Message));
                    return View(new PagedResult<UserViewModel>());
                }

                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "User-Create")]
        [HttpGet("user/create")]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userService.Create(request);

                if (result.IsSuccessed)
                {
                    ViewBag.Message = ConstantHelper.InsertSuccess;
                    return View(request);
                }

                ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(result.Message) ? ConstantHelper.UpdateError : result.Message);

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "User-Edit")]
        [HttpGet("user/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _userService.GetById(id);
                if (!result.IsSuccessed) {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(new UserEditRequest(result.ResultObj));
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userService.Update(request);

                if (result.IsSuccessed)
                {
                    ViewBag.Message = ConstantHelper.UpdateSuccess;
                    return View(request);
                }

                ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(result.Message) ? ConstantHelper.UpdateError : result.Message);

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "User-Delete")]
        [HttpGet("user/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(new UserDeleteRequest() { 
                    Id = result.ResultObj.Id,
                    UserName = result.ResultObj.UserName,
                    FullName = result.ResultObj.FullName
                });
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userService.Delete(request.Id);

                if (result.IsSuccessed)
                {
                    ViewBag.Javascript = ConstantHelper.JSDeleteDone;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(result.Message) ? ConstantHelper.UpdateError : result.Message);
                }

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "User-Details")]
        [HttpGet("user/details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _userService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "User-AssignRole")]
        [HttpGet("user/assignrole/{id}")]
        public async Task<IActionResult> AssignRole(int id)
        {
            try
            {
                var result = await _userService.GetUserRoles(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserAssignRoleRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userService.AssignRole(request);

                if (result.IsSuccessed)
                {
                    ViewBag.Message = ConstantHelper.UpdateSuccess;
                    return View(request);
                }

                ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(result.Message) ? ConstantHelper.UpdateError : result.Message);

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "User-AssignRoleGroup")]
        [HttpGet("user/assignrolegroup/{id}")]
        public async Task<IActionResult> AssignRoleGroup(int id)
        {
            try
            {
                var result = await _userService.GetUserRoleGroups(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleGroup(UserAssignRoleGroupRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userService.AssignRoleGroup(request);

                if (result.IsSuccessed)
                {
                    ViewBag.Message = ConstantHelper.UpdateSuccess;
                    return View(request);
                }

                ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(result.Message) ? ConstantHelper.UpdateError : result.Message);

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
    }
}
