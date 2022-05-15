/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.RoleClaims;
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
    public class RoleClaimController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleClaimService _roleClaimService;
        private readonly IRoleService _roleService;

        public RoleClaimController(IConfiguration configuration, 
            IRoleClaimService roleClaimService,
            IRoleService roleService)
        {
            _configuration = configuration;
            _roleClaimService = roleClaimService;
            _roleService = roleService;
        }

        [Authorize(Roles = "RoleClaim-Index")]
        [HttpGet("roleClaim/index/{roleId}")]
        public async Task<IActionResult> Index(int roleId, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.Keyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword;
                var request = new GetRoleClaimPagingRequest()
                {
                    RoleId = roleId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Keyword = keyword
                };

                var role = (await _roleService.GetById(roleId)).ResultObj;
                var result = await _roleClaimService.GetListPaging(request);

                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, (string.IsNullOrEmpty(result.Message) ? ConstantHelper.LoadingError : result.Message));
                    return View(new PagedResult<RoleClaimViewModel>());
                }

                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "RoleClaim-Create")]
        [HttpGet("roleClaim/create/{roleId}")]
        public async Task<IActionResult> Create(int roleId)
        {
            try
            {
                var role = (await _roleService.GetById(roleId)).ResultObj;
                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                return View();
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleClaimCreateRequest request)
        {
            try
            {
                var role = (await _roleService.GetById(request.RoleId)).ResultObj;
                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleClaimService.Create(request);

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

        [Authorize(Roles = "RoleClaim-Edit")]
        [HttpGet("roleClaim/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _roleClaimService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    ViewBag.RoleName = string.Empty;
                    ViewBag.RoleId = string.Empty;

                    return View();
                }

                var role = (await _roleService.GetById(result.ResultObj.RoleId)).ResultObj;
                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                return View(new RoleClaimEditRequest(result.ResultObj));
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleClaimEditRequest request)
        {
            try
            {
                var role = (await _roleService.GetById(request.RoleId)).ResultObj;
                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleClaimService.Update(request);

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

        [Authorize(Roles = "RoleClaim-Delete")]
        [HttpGet("roleClaim/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _roleClaimService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    ViewBag.RoleName = string.Empty;
                    ViewBag.RoleId = string.Empty;

                    return View();
                }

                var role = (await _roleService.GetById(result.ResultObj.RoleId)).ResultObj;
                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                return View(new RoleClaimDeleteRequest()
                {
                    Id = result.ResultObj.Id,
                    RoleId = result.ResultObj.RoleId,
                    ClaimType = result.ResultObj.ClaimType,
                    ClaimValue = result.ResultObj.ClaimValue
                });
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleClaimDeleteRequest request)
        {
            try
            {
                var role = (await _roleService.GetById(request.RoleId)).ResultObj;
                ViewBag.RoleName = role == null || string.IsNullOrEmpty(role.Description) ? string.Empty : role.Description;
                ViewBag.RoleId = role == null || string.IsNullOrEmpty(role.Name) ? string.Empty : role.Id.ToString();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleClaimService.Delete(request.Id);

                if (result.IsSuccessed)
                {
                    ViewBag.Javascript = ConstantHelper.JSDeleteDone;
                    //return RedirectToAction("index", new { roleId = ViewBag.RoleId });
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
    }
}
