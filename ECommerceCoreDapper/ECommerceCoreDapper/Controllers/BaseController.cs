using Microsoft.AspNetCore.Mvc;

namespace ECommerceCoreDapper.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.logon = "My Account";
        }
    }
}
