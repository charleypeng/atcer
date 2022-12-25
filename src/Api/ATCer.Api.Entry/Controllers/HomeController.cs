// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATCer.Web.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [IgnoreAudit]
        public IActionResult Index()
        {
            return View();
        }
    }
}
