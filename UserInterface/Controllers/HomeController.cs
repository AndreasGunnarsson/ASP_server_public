using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserInterface.Models;
/* using MySqlConnector; */
/* using MySqlConnector.Authentication.Ed25519; */
using Dapper;
using Core.Entities;
using Core.Interfaces;
using Application;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreateUserService _createuserservice;
        private readonly ILoginUserService _loginuserservice;
        /* private AppDb Db { get; set; } */
        /* private readonly IAppDb _db; */
        /* private readonly IUserBaseService _userBaseService; */ 

        /* public HomeController(ILogger<HomeController> logger, IAppDb db, IUserBaseService ubs) */
        public HomeController(ILogger<HomeController> logger, ICreateUserService createuserservice, ILoginUserService loginuserservice)
        {
            /* Ed25519AuthenticationPlugin.Install(); */
            /* _db = db; */
            _logger = logger;
            _createuserservice = createuserservice;
            _loginuserservice = loginuserservice;
            /* _userBaseService = ubs; */
        }

        public IActionResult Index()
        {
            return View();
        }

        /* public IActionResult Fisdex(MySqlConnection connection) */
        /* { */
        /*     Console.WriteLine("Fisdex"); */
        /*     connection.Open(); */
        /*     using var command = new MySqlCommand("SELECT * FROM Roles;", connection); */
        /*     return View(); */ 
            
        /* } */

        public IActionResult Privacy()
        {
            /* Db.Connection.Open(); */

            /* _userBaseService.TESTMETHOD();          // Debug/testing. */
            /* string sql = "SELECT * FROM Roles"; */

            /* var roles = _db.Connection.Query(sql); */

            /* foreach (var f in roles) */
            /* { */
            /*     Console.WriteLine(f); */
            /* } */
            /* Console.WriteLine(roles); */
            /* FiddleHelper.WriteTable(orderDetail); */

            /* using var connection = new MySqlConnection(); */
            Console.WriteLine("Privacy");           // Debug.
            /* connection.Open(); */
            /* using var command = new MySqlCommand("SELECT * FROM Roles;", connection); */
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            Console.WriteLine("CreateUser GET");                        // Debug.
            // Denna action körs vid en GET.
            // Ska inte gå att köra om man redan är inloggad.
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUser model)
        {
            Console.WriteLine("CreateUser POST: " + model.UserName + " " + model.Password);       // Debug.

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState not valid.");         // Debug.
                return View(); 
            }

            else
            {
                AccountsTemp plainTextAccount = new AccountsTemp(model.UserName, model.Password);
                _createuserservice.CreateUser(plainTextAccount);
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
            Console.WriteLine("LoginUser GET");                        // Debug.
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(LoginUser model)
        {
            Console.WriteLine("LoginUser POST");                        // Debug.
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState not valid.");         // Debug.
                return View(); 
            }

            else
            {
                AccountsTemp accountLogin = new AccountsTemp(model.UserName, model.Password);
                _loginuserservice.LoginUser(accountLogin);
                return View();

                // TODO: Läs om "over posting" ([Bind]).
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
