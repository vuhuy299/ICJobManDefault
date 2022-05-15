/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.BaseModels.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ICSLib.JobManApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result) {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
