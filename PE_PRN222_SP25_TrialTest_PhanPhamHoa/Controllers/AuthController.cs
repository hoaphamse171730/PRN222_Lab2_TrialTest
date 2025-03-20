using Business_Logic.Interfaces;
using Business_Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE_PRN222_SP25_TrialTest_PhanPhamHoa.Models;

namespace PE_PRN222_SP25_TrialTest_PhanPhamHoa.Controllers
{

    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = await _authService.AuthenticateAsync(model.Email, model.Password);

            if (account != null && (account.Role == 2 || account.Role==3))
            {
                HttpContext.Session.SetString("Role", account.Role.ToString());
                HttpContext.Session.SetString("Email", account.EmailAddress.ToString());

                TempData["Message"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "You do not have permission to do this function!");
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("Email") != null || HttpContext.Session.GetString("Role") != null )
            {
                HttpContext.Session.Remove("Email");
                HttpContext.Session.Remove("Role");

                return RedirectToAction("Index", "Home");
            }
            return View();
        }


    }


}
