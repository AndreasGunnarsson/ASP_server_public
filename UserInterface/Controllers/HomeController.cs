using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserInterface.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreateUserService _createUserService;
        private readonly ILoginUserService _loginUserService;
        /* private readonly IUserBaseService _userBaseService; */ 

        /* public HomeController(ILogger<HomeController> logger, IAppDb db, IUserBaseService ubs) */
        public HomeController(ILogger<HomeController> logger, ICreateUserService createuserservice, ILoginUserService loginuserservice)
        {
            _logger = logger;
            _createUserService = createuserservice;
            _loginUserService = loginuserservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            /* ViewData["FFF"] = "HelloFFF"; */
            /* ViewData["GGG"] = "HelloGGG"; */
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            // Ska inte gå att köra om man redan är inloggad.
            var cookie = Request.Cookies["SessionId"];          // Debug.
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUser model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            else
            {
                AccountsTemp plainTextAccount = new AccountsTemp(model.UserName, model.Password);
                _createUserService.CreateUser(plainTextAccount);
                return View();
                // Ska finnas form.
                    // Användarnamn
                    // Lösenord
                    // Confirm password (vet inte ens om jag behöver hantera det här; hantera endast i JavaScript).
                        // Submit ska inte fungera ifall password:et inte matchar; coolast vore realtids-check.
                // Måste ta input-parametrar alternativt ett objekt.
                // Använd sedan logik från Application-lagret.
                // Ska inte gå att köra om man redan är inloggad (Behöver authorization-attribut).

                // TODO: Skicka en till en View som confirmar att accountet är skapat.
                // TODO: Läs om "over posting" ([Bind]).
            }
        }

        [HttpGet]
        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(LoginUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(); 
            }

            else
            {
                AccountsTemp accountLogin = new AccountsTemp(model.UserName, model.Password);
                var sessionId = _loginUserService.LoginUser(accountLogin);

                if (sessionId != null)
                {
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.HttpOnly = true;
                    cookieOptions.SameSite = SameSiteMode.Strict;
                    Response.Cookies.Append("SessionId", sessionId, cookieOptions);

                    // TODO: Gå till en annan sida; t.ex. home efter lyckad inloggning.
                        // Skriv ut vem som är inloggad högst upp i högra hörnet på sidan.
                    /* return View(); */
                }
                return View();
            }
        }

        [HttpPost]
        public void LogoutButton()
        {
            var sessionId = Request.Cookies["SessionId"];
            _loginUserService.LogoutUser(sessionId);
            Response.Cookies.Delete("SessionId");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
