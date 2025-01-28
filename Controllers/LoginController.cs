using Microsoft.AspNetCore.Mvc;
using MonitorTool.Data;
using MonitorTool.Models;

namespace MonitorTool.Controllers
{

    public class LoginController : Controller
    {

        private readonly MonitorToolDbContext _context;

        public LoginController(MonitorToolDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Check(LoginViewModel model)
        {
     

            if (ModelState.IsValid) 
            {
                var user = _context.Login.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    return RedirectToAction("Index", "Student");
                }
                ModelState.AddModelError("", "Invalid email or password");
            }
            //else
            //{
            //    alert
            //}
            return RedirectToAction(nameof(Index));
        }
    }
}
