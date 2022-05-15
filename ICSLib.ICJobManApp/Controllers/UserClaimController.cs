/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.UserClaims;
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
    public class UserClaimController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserClaimService _userClaimService;
        private readonly IUserService _userService;

        public UserClaimController(IConfiguration configuration,
            IUserClaimService userClaimService,
            IUserService userService)
        {
            _configuration = configuration;
            _userClaimService = userClaimService;
            _userService = userService;
        }

        [Authorize(Roles = "UserClaim-Index")]
        [HttpGet("userClaim/index/{userId}")]
        public async Task<IActionResult> Index(int userId, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                ViewBag.Keyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword;
                var request = new GetUserClaimPagingRequest()
                {
                    UserId = userId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Keyword = keyword
                };

                var user = (await _userService.GetById(userId)).ResultObj;
                var result = await _userClaimService.GetListPaging(request);

                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, (string.IsNullOrEmpty(result.Message) ? ConstantHelper.LoadingError : result.Message));
                    return View(new PagedResult<UserClaimViewModel>());
                }

                return View(result.ResultObj);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [Authorize(Roles = "UserClaim-Create")]
        [HttpGet("userClaim/create/{userId}")]
        public async Task<IActionResult> Create(int userId)
        {
            try
            {
                var user = (await _userService.GetById(userId)).ResultObj;
                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                return View();
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserClaimCreateRequest request)
        {
            try
            {
                var user = (await _userService.GetById(request.UserId)).ResultObj;
                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userClaimService.Create(request);

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

        [Authorize(Roles = "UserClaim-Edit")]
        [HttpGet("userClaim/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _userClaimService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    ViewBag.UserName = string.Empty;
                    ViewBag.UserId = string.Empty;

                    return View();
                }

                var user = (await _userService.GetById(result.ResultObj.UserId)).ResultObj;
                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                return View(new UserClaimEditRequest(result.ResultObj));
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserClaimEditRequest request)
        {
            try
            {
                var user = (await _userService.GetById(request.UserId)).ResultObj;
                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userClaimService.Update(request);

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

        [Authorize(Roles = "UserClaim-Delete")]
        [HttpGet("userClaim/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userClaimService.GetById(id);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    ViewBag.UserName = string.Empty;
                    ViewBag.UserId = string.Empty;

                    return View();
                }

                var user = (await _userService.GetById(result.ResultObj.UserId)).ResultObj;
                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                return View(new UserClaimDeleteRequest()
                {
                    Id = result.ResultObj.Id,
                    UserId = result.ResultObj.UserId,
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
        public async Task<IActionResult> Delete(UserClaimDeleteRequest request)
        {
            try
            {
                var user = (await _userService.GetById(request.UserId)).ResultObj;
                ViewBag.UserName = user == null || string.IsNullOrEmpty(user.FullName) ? string.Empty : user.FullName;
                ViewBag.UserId = user == null || string.IsNullOrEmpty(user.UserName) ? string.Empty : user.Id.ToString();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _userClaimService.Delete(request.Id);

                if (result.IsSuccessed)
                {
                    ViewBag.Javascript = ConstantHelper.JSDeleteDone;
                    //return RedirectToAction("index", new { userId = ViewBag.UserId });
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
