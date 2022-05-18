using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserInterface.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreateUserService _createUserService;
        private readonly ILoginUserService _loginUserService;
        private readonly ICreateArticleService _createArticleService;
        private readonly ICommentsService _commentsService;

        public HomeController(ILogger<HomeController> logger, ICreateUserService createUserService, ILoginUserService loginUserService, ICreateArticleService articleService, ICommentsService commentsService)
        {
            _logger = logger;
            _createUserService = createUserService;
            _loginUserService = loginUserService;
            _createArticleService = articleService;
            _commentsService = commentsService;
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
        public IActionResult Article([FromRoute]int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Article not found.");
            }

            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);

            var articles = _createArticleService.ReadAllArticles();
            var article = articles.FirstOrDefault(x => x.Id == id.Value);
            if (article == null)
            {
                return NotFound("Article not found.");
            }

            var comments = _commentsService.ReadArticleComments(id.Value);

            ViewData["Comments"]  = new List<CommentWithName>(comments);
            ViewData["Article"]  = article;
            ViewData["isLoggedIn"]  = userSession != null ? true : false;

            return View();
        }

        [HttpPost]
        public IActionResult Article(ArticleComment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad post");
            }

            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null)
            {
                bool canCreate = false;
                if (comment.CommentsIdzz.HasValue)
                    canCreate = _commentsService.CanCreateComment(comment.Articleeeid, comment.CommentsIdzz.Value);
                else
                    canCreate = _commentsService.CanCreateComment(comment.Articleeeid);
                if (canCreate)
                {
                    _commentsService.CreateComment(new Comments() {
                        Comment = comment.Commentzz,
                        AccountsId = userSession.userId,
                        ArticlesId = comment.Articleeeid,
                        CommentsId = comment.CommentsIdzz.HasValue ? comment.CommentsIdzz.Value : null
                    });
                }
            }

            return Ok("Article OK!");
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

        [HttpGet]
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

        [HttpGet]
        public IActionResult UserSettings()
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null)
            {
                var comments = _commentsService.ReadAccountComments(userSession.userId);
                ViewData["Comments"]  = new List<Comments>(comments);
                return View();
            }
            else
                return NotFound("No session found!");

        }

        [HttpPost]
        public IActionResult UpdateUserPassword(UpdatePassword updatePassword)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("NotFound");
            }

            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null)
            {
                _createUserService.UpdatePassword(updatePassword.OldPassword, updatePassword.NewPassword, userSession.userId);
            }

            return NotFound("NotFound");
        }

        [HttpPost]
        public IActionResult RemoveComment([FromQuery]int commentId)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("fail");
            }

            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null)
            {
                _commentsService.DeleteComment(commentId, userSession.userId);
            }

            return NotFound("NotFound");
        }

        [HttpPost]
        public IActionResult LoginUser(LoginUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(); 
            }

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

        [HttpPost]
        public void Logout()
        {
            var sessionId = Request.Cookies["SessionId"];
            _loginUserService.LogoutUser(sessionId);
            Response.Cookies.Delete("SessionId");
        }

        [HttpPost]
        public void RemoveUser()
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null)
            {
                _createUserService.RemoveUser(userSession.userId);
            }
            Response.Cookies.Delete("SessionId");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
