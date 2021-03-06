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

        public string ReadArticleFile(int id)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            bool isFileExist = System.IO.File.Exists($"{path}/ASP_server/{id}.html");
            if (isFileExist)
            {
                string lines = System.IO.File.ReadAllText($"{path}/ASP_server/{id}.html");
                return lines;
            }
            else
                return null;
        }

        public void CreateArticle(ArticleTransfer article)
        {
            var newArticle = new Articles() {
                Title = article.Title,
            };
            var newId = _articleRepository.CreateArticle(newArticle);

            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            System.IO.File.WriteAllText($"{path}/ASP_server/{newId}.html", article.Article);
        }

        public void UpdateArticle(ArticleTransfer article)
        {
            if (!String.IsNullOrWhiteSpace(article.Title))
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                System.IO.File.WriteAllText($"{path}/ASP_server/{article.Id}.html", article.Article);
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
