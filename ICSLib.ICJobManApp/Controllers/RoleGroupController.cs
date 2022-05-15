/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.RoleGroups;
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
    public class RoleGroupController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleGroupService _roleGroupService;
        private readonly IRoleService _roleService;

        public RoleGroupController(IConfiguration configuration,
            IRoleGroupService roleGroupService,
            IRoleService roleService)
        {
            _configuration = configuration;
            _roleGroupService = roleGroupService;
            _roleService = roleService;
        }

        [Authorize(Roles = "RoleGroup-Index")]
        [HttpGet("rolegroup/index")]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.Keyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword;
                var request = new GetRoleGroupPagingRequest()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Keyword = keyword
                };

                var result = await _roleGroupService.GetListPaging(request);

                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, (string.IsNullOrEmpty(result.Message) ? ConstantHelper.LoadingError : result.Message));
                    return View(new PagedResult<RoleGroupViewModel>());
                }

                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return View();
        }

        [Authorize(Roles = "RoleGroup-Create")]
        [HttpGet("rolegroup/create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleGroupCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleGroupService.Create(request);

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
            }
            return View();
        }

        [Authorize(Roles = "RoleGroup-Edit")]
        [HttpGet("rolegroup/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _roleGroupService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);

                    return View();
                }

                return View(new RoleGroupEditRequest(result.ResultObj));
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleGroupEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleGroupService.Update(request);

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
            }
            return View();
        }

        [Authorize(Roles = "RoleGroup-Delete")]
        [HttpGet("rolegroup/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _roleGroupService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(new RoleGroupDeleteRequest()
                {
                    RoleGroupId = result.ResultObj.RoleGroupId,
                    RoleGroupName = result.ResultObj.RoleGroupName,
                    RoleGroupDesc = result.ResultObj.RoleGroupDesc
                });
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleGroupDeleteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleGroupService.Delete(request.RoleGroupId);

                if (result.IsSuccessed)
                {
                    ViewBag.Javascript = ConstantHelper.JSDeleteDone;
                    //return RedirectToAction("index");
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
            }
            return View();
        }

        [Authorize(Roles = "RoleGroup-Details")]
        [HttpGet("rolegroup/details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _roleGroupService.GetById(id);
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
            }
            return View();
        }

        [Authorize(Roles = "RoleGroup-AssignRole")]
        [HttpGet("rolegroup/assignrole/{id}")]
        public async Task<IActionResult> AssignRole(int id)
        {
            try
            {
                var result = await _roleGroupService.GetWithRolesById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }

                return View(new RoleGroupAssignRoleRequest()
                {
                    RoleGroupId = result.ResultObj.RoleGroupId,
                    RoleGroupName = result.ResultObj.RoleGroupName,
                    Roles = result.ResultObj.Roles
                });
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleGroupAssignRoleRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                var result = await _roleGroupService.AssignRole(request);

                if (result.IsSuccessed)
                {
                    ViewBag.Message = "Cập nhật Chức năng cho Quyền thành công.";
                    return View(request);
                }

                ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(result.Message) ? ConstantHelper.UpdateError : result.Message);

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return View();
        }
    }
}
