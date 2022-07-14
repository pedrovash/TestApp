using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers
{
    public class MenuAdminController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "MenuAdmin");
            }

            
        }


      
        
    }
}
