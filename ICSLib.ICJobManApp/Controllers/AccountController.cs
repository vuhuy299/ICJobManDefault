/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Users;
using ICSLib.Authen.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ICSLib.JobManApp.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AccountController(//IUserApiClient userApiClient, 
            IConfiguration configuration,
            IUserService userService)
        {
            //_userApiClient = userApiClient;
            _configuration = configuration;
            _userService = userService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] string returnUrl)
        {
            try
            {
                await _userService.SignOut();
                
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Remove(_configuration["SessionKeys:Token"]);
                
                ViewBag.ReturnUrl = string.IsNullOrEmpty(returnUrl) ? string.Empty : returnUrl;
                
                return View();
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl, LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                //using api
                //var result = await _userApiClient.Authenticate(request);
                //using DI
                var result = await _userService.Authenticate(request);

                if (result.IsSuccessed && !string.IsNullOrEmpty(result.ResultObj)) {
                    var token = result.ResultObj;
                    var userPrincipal = this.ValidateToken(token);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties
                        );

                    string menu = userPrincipal.FindFirstValue(ClaimTypes.UserData);

                    HttpContext.Session.SetString(_configuration["SessionKeys:Token"], token);
                    HttpContext.Session.SetString(_configuration["SessionKeys:Menu"], menu);

                    if (!string.IsNullOrEmpty(request.ReturnUrl))
                    {
                        return Redirect(request.ReturnUrl);
                    }

                    if (!string.IsNullOrEmpty(returnUrl)) {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("index", "home");
                }
                if (!string.IsNullOrEmpty(result.Message)) {
                    if (result.Message.ToLower().Contains("mật khẩu")
                        || result.Message.ToLower().Contains("password"))
                    {
                        ModelState.AddModelError("Password", result.Message);
                    }
                    else {
                        ModelState.AddModelError("UserName", result.Message);
                    }
                }

                return View(request);
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = true;
                validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
                validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.SignOut();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Remove(_configuration["SessionKeys:Token"]);
                return RedirectToAction("login", "account");
            }
            catch (Exception ex)
            {
                ICSLib.Utilities.Helpers.LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                throw;
            }
        }

        public async Task<IActionResult> AccessDenied() {
            return View();
        }
    }
}
