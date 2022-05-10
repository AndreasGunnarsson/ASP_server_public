using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserInterface.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreateUserService _createUserService;
        private readonly ILoginUserService _loginUserService;
        private readonly ICreateArticleService _createArticleService;

        public HomeController(ILogger<HomeController> logger, ICreateUserService createUserService, ILoginUserService loginUserService, ICreateArticleService articleService)
        {
            _logger = logger;
            _createUserService = createUserService;
            _loginUserService = loginUserService;
            _createArticleService = articleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var articles = _createArticleService.ReadAllArticles();
            var articlesModel = new UserInterface.Models.Article();
            articlesModel.articles = articles;

            return View(articlesModel);
        }

        [HttpGet]
        public IActionResult Article(int? id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateArticle(int? id)
        {
            if (id.HasValue)
            {
                var articles = _createArticleService.ReadAllArticles();
                var singleArticle = articles.FirstOrDefault(x => x.Id == id);

                if (singleArticle != null)
                {
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string lines = System.IO.File.ReadAllText($"{path}/ASP_server/UserInterface/Views/Shared/_{id.Value}.cshtml");
                    var createArticle = new UserInterface.Models.CreateArticle() { Id = id.Value, Titleee = singleArticle.Title, Articleee = lines };
                    return View(createArticle);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticle article)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (article.Id <= 0)
            {
                _createArticleService.CreateArticle(new ArticleTransfer() { Title = article.Titleee, Article = article.Articleee });
            }
            else if (article.Id > 0)
            {
                _createArticleService.UpdateArticle(new ArticleTransfer() { Id = article.Id, Title = article.Titleee, Article = article.Articleee });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null && userSession.userRole == 1)
                return Ok("Successful auth. Role 1");
            if (userSession != null && userSession.userRole == 2)
                return View();
            else
                return NotFound("Authorization failed!");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUser model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AccountsTemp plainTextAccount = new AccountsTemp(model.UserName, model.Password);
            _createUserService.CreateUser(plainTextAccount);
            return View();
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
                }
                return View();
            }
        }

        [HttpPost]
        public void Logout()
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
