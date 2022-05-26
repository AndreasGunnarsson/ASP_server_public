using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserInterface.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
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
            string lines = _createArticleService.ReadArticleFile(id.Value);
            if (lines == null)
            {
                return NotFound("Article not found.");
            }

            ViewData["Comments"]  = new List<CommentWithName>(comments);
            ViewData["Article"]  = article;
            ViewData["articleText"] = lines;
            ViewData["isLoggedIn"]  = userSession != null ? true : false;

            return View();
        }

        [HttpPost]
        public IActionResult Article(ArticleComment comment)
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (!ModelState.IsValid)
            {
                /* return BadRequest("Bad post"); */
                var articles = _createArticleService.ReadAllArticles();
                var article = articles.FirstOrDefault(x => x.Id == comment.Articleeeid);
                if (article == null)
                {
                    return NotFound("Article not found.");
                }

                var comments = _commentsService.ReadArticleComments(comment.Articleeeid);
                string lines = _createArticleService.ReadArticleFile(comment.Articleeeid);
                if (lines == null)
                {
                    return NotFound("Article not found.");
                }

                ViewData["Comments"]  = new List<CommentWithName>(comments);
                ViewData["Article"]  = article;
                ViewData["articleText"] = lines;
                ViewData["isLoggedIn"]  = userSession != null ? true : false;

                return View();
            }

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

                var articles = _createArticleService.ReadAllArticles();
                var article = articles.FirstOrDefault(x => x.Id == comment.Articleeeid);
                if (article == null)
                {
                    return NotFound("Article not found.");
                }

                var comments = _commentsService.ReadArticleComments(comment.Articleeeid);
                string lines = _createArticleService.ReadArticleFile(comment.Articleeeid);
                if (lines == null)
                {
                    return NotFound("Article not found.");
                }

                ViewData["Comments"]  = new List<CommentWithName>(comments);
                ViewData["Article"]  = article;
                ViewData["articleText"] = lines;
                ViewData["isLoggedIn"]  = userSession != null ? true : false;

                return View();
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Admin()
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null && userSession.userRole == 1)
            {
                var articles = _createArticleService.ReadAllArticles();
                var articlesModel = new UserInterface.Models.Article();
                articlesModel.articles = articles;

                return View(articlesModel);
            }
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult CreateArticle(int? id)
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null && userSession.userRole == 1)
            {
                if (id.HasValue)
                {
                    var articles = _createArticleService.ReadAllArticles();
                    var singleArticle = articles.FirstOrDefault(x => x.Id == id);

                    if (singleArticle != null)
                    {
                        string lines = _createArticleService.ReadArticleFile(id.Value);
                        if (lines == null)
                        {
                            return NotFound("Could not read article file.");
                        }
                        var createArticle = new UserInterface.Models.CreateArticle() { Id = id.Value, Titleee = singleArticle.Title, Articleee = lines };
                        return View(createArticle);
                    }
                }
                return View();
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticle article)
        {
            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null && userSession.userRole == 1)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (article.Id <= 0)
                {
                    _createArticleService.CreateArticle(new ArticleTransfer() { Title = article.Titleee, Article = article.Articleee });
                    ViewData["InfoMessage"] = "Article created.";
                }
                else if (article.Id > 0)
                {
                    _createArticleService.UpdateArticle(new ArticleTransfer() { Id = article.Id, Title = article.Titleee, Article = article.Articleee });
                    ViewData["InfoMessage"] = "Article updated.";
                }
                return View();
            }
            else
                return NotFound();
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
                ViewData["ErrorMessage"] = "Account creation failed.";
                return View();
            }

            AccountsTemp plainTextAccount = new AccountsTemp(model.UserName, model.Password);
            bool isCreated = _createUserService.CreateUser(plainTextAccount);
            if (isCreated)
                ViewData["InfoMessage"] = "Account created successfully.";
            else if (!isCreated)
                ViewData["ErrorMessage"] = "Account was not created.";
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

            AccountsTemp accountLogin = new AccountsTemp(model.UserName, model.Password);
            var sessionId = _loginUserService.LoginUser(accountLogin);

            if (sessionId != null)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.HttpOnly = true;
                cookieOptions.SameSite = SameSiteMode.Strict;
                Response.Cookies.Append("SessionId", sessionId, cookieOptions);
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "Login failed";
            }

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
                return NotFound();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var sessionId = Request.Cookies["SessionId"];
            _loginUserService.LogoutUser(sessionId);
            Response.Cookies.Delete("SessionId");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UserSettings(UpdatePassword updatePassword)
        {

            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (!ModelState.IsValid && userSession != null)
            {
                var comments = _commentsService.ReadAccountComments(userSession.userId);
                ViewData["Comments"]  = new List<Comments>(comments);
                return View();
            }

            if (userSession != null)
            {
                var isUpdated = _createUserService.UpdatePassword(updatePassword.OldPassword, updatePassword.NewPassword, userSession.userId);
                if(isUpdated)
                    TempData["MessageInfo"] = "Password updated successfully.";
                else
                    TempData["MessageError"] = "Password was not updated.";
                return RedirectToAction("UserSettings");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveComment([FromQuery]int commentId)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Could not remove comment.");
            }

            var sessionId = Request.Cookies["SessionId"];
            var userSession = _loginUserService.CheckLogin(sessionId);
            if (userSession != null)
            {
                _commentsService.DeleteComment(commentId, userSession.userId);
            }

            return Ok($"Removed comment.");
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
