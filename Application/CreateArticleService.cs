using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CreateArticleService : ICreateArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public CreateArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IEnumerable<Articles> ReadAllArticles()
        {
            return _articleRepository.ReadAllArticles();
        }

        public void CreateArticle(ArticleTransfer article)
        {
            var newArticle = new Articles() {
                Title = article.Title,
            };
            var newId = _articleRepository.CreateArticle(newArticle);

            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            System.IO.File.WriteAllText($"{path}/ASP_server/UserInterface/Views/Shared/_{newId}.cshtml", article.Article);
        }

        public void UpdateArticle(ArticleTransfer article)
        {
            if (!String.IsNullOrWhiteSpace(article.Title))
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                System.IO.File.WriteAllText($"{path}/ASP_server/UserInterface/Views/Shared/_{article.Id}.cshtml", article.Article);
                var newArticle = new Articles() {
                    Id = article.Id,
                    Title = article.Title,
                    EditDate = DateTime.Now
                };

                _articleRepository.UpdateArticle(newArticle);
            }
        }

        public void AddCategory(IEnumerable<ArticlesCategories> articlesCategories)
        {
        }
    }
}
