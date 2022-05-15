/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ICSLib.JobManApp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //string token = context.HttpContext.Session.GetString("Token");
            string menu = context.HttpContext.Session.GetString("Menu");
            if (string.IsNullOrEmpty(menu))
            {
                context.Result = new RedirectToActionResult("login", "account", null);
            }
            List<RoleViewModel> menus = new List<RoleViewModel>();
            if (!string.IsNullOrEmpty(menu))
            {
                menus = JsonConvert.DeserializeObject<List<RoleViewModel>>(menu);
                menus.ForEach(m => { m.Selected = false; });

                var action = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
                var controller = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;

                var curr_menu = menus.Where(m => m.Controler.Equals(controller) && m.Action.Equals(action))
                    .FirstOrDefault();
                if (curr_menu != null)
                {
                    HttpContext.Session.SetString("curr_menu", JsonConvert.SerializeObject(curr_menu));
                }
                else
                {
                    string curr_menu_json = context.HttpContext.Session.GetString("curr_menu");
                    if (!string.IsNullOrEmpty(curr_menu_json))
                    {
                        curr_menu = JsonConvert.DeserializeObject<RoleViewModel>(curr_menu_json);
                        menus.Where(m => m.Id == curr_menu.Id).ToList().ForEach(m => { m.Selected = true; });
                    }
                }
                if (curr_menu != null)
                {
                    curr_menu.Selected = true;
                    while (curr_menu.ParentRoleId > 0)
                    {
                        curr_menu = menus.Where(m => m.Id == curr_menu.ParentRoleId)
                            .FirstOrDefault();
                        if (curr_menu == null) break;
                        curr_menu.Selected = true;
                    }
                }

                ViewBag.Menu = JsonConvert.SerializeObject(menus);
            }
            else
            {
                ViewBag.Menu = "[]";
            }
            base.OnActionExecuting(context);
        }
    }
}
