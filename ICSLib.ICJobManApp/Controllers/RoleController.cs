/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Roles;
using ICSLib.Authen.Services.Interfaces;
using ICSLib.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICSLib.JobManApp.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public RoleController(IConfiguration configuration, IRoleService roleService)
        {
            _configuration = configuration;
            _roleService = roleService;
        }

        [Authorize(Roles = "Role-Index")]
        [HttpGet("role/index")]
        public async Task<IActionResult> Index(string keyword, int parentId = 0, byte isShow = 2, byte statusId = 2)
        {
            try
            {
                keyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword;
                ViewBag.Keyword = keyword;
                var request = new GetRoleHierarchyRequest()
                {
                    ParentRoleId = parentId,
                    Keyword = keyword,
                    IsShow = isShow,
                    StatusId = statusId
                };

                var result = await _roleService.GetHỉerachy(request);

                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, (string.IsNullOrEmpty(result.Message) ? ConstantHelper.LoadingError : result.Message));
                    return View(new List<RoleViewModel>());
                }

                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "Role-Create")]
        [HttpGet("role/create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var roles = (await _roleService.GetHỉerachy(new GetRoleHierarchyRequest())).ResultObj;
                roles = roles == null ? new List<RoleViewModel>() : roles;
                var role = new RoleViewModel()
                {
                    Id = 0,
                    Description = "Chọn chức năng cha"
                };
                roles.Insert(0, role);
                ViewBag.Roles = new SelectList(roles, "Id", "Description", 0);

                return View();
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateRequest request)
        {
            try
            {
                var roles = (await _roleService.GetHỉerachy(new GetRoleHierarchyRequest())).ResultObj;
                roles = roles == null ? new List<RoleViewModel>() : roles;
                var role = new RoleViewModel()
                {
                    Id = 0,
                    Description = "Chọn chức năng cha"
                };
                roles.Insert(0, role);
                ViewBag.Roles = new SelectList(roles, "Id", "Description", request.ParentRoleId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleService.Create(request);

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

        [Authorize(Roles = "Role-Edit")]
        [HttpGet("role/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _roleService.GetById(id);

                var roles = (await _roleService.GetHỉerachy(new GetRoleHierarchyRequest())).ResultObj;
                roles = roles == null ? new List<RoleViewModel>() : roles;
                var role = new RoleViewModel()
                {
                    Id = 0,
                    Description = "Chọn chức năng cha"
                };
                roles.Insert(0, role);
                ViewBag.Roles = new SelectList(roles, "Id", "Description", result.ResultObj == null ? 0 : result.ResultObj.ParentRoleId);

                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }

                return View(new RoleEditRequest(result.ResultObj));
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditRequest request)
        {
            try
            {
                var roles = (await _roleService.GetHỉerachy(new GetRoleHierarchyRequest())).ResultObj;
                roles = roles == null ? new List<RoleViewModel>() : roles;
                var role = new RoleViewModel()
                {
                    Id = 0,
                    Description = "Chọn chức năng cha"
                };
                roles.Insert(0, role);
                ViewBag.Roles = new SelectList(roles, "Id", "Description", request.ParentRoleId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleService.Update(request);

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

        [Authorize(Roles = "Role-Delete")]
        [HttpGet("role/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _roleService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                return View(new RoleDeleteRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name,
                    Description = result.ResultObj.Description
                });
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleDeleteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _roleService.Delete(request.Id);

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
                throw;
            }
        }

        [Authorize(Roles = "Role-Details")]
        [HttpGet("role/details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _roleService.GetWithClaimsById(id);
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
    }
}
