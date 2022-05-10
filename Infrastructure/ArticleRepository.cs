using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Dapper;

namespace Infrastructure
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IAppDb _db;

        public ArticleRepository(IAppDb db)
        {
            _db = db;
        }

        public void CreateCategory(Categories category) {}
        public IEnumerable<Categories> ReadCategory()
        {
            return null;
        }
        public void UpdateCategory(Categories category) {}
        public void DeleteCategory(Categories category) {}

        public ulong CreateArticle(Articles article)
        {
            string sql = "INSERT INTO Articles (Title) VALUES (@ArticleTitle); SELECT LAST_INSERT_ID();";
            var parameters = new {
                ArticleTitle = article.Title,
            };

            var newId = _db.Connection.QueryFirstOrDefault<ulong>(sql, parameters);
            return newId;
        }

        public void UpdateArticle(Articles article)
        {
            string sql = "UPDATE Articles SET Title = @NewTitle, EditDate = @NewEditDate WHERE Id=@ArticleId;";
            var parameters = new {
                ArticleId = article.Id,
                NewTitle = article.Title,
                NewEditDate = article.EditDate
            };

            _db.Connection.Execute(sql, parameters);
        }

        public IEnumerable<Articles> ReadAllArticles()
        {
            string sql = "SELECT * FROM Articles";

            var articles = _db.Connection.Query<Articles>(sql);

            return articles;
        }

        public IEnumerable<int> ReadCategoriesByArticlesId(int articlesId)
        {
            return null;
        }

        public void CreateArticlesCategory(ArticlesCategories articleCategory) {}
        public void RemoveArticlesCategory(ArticlesCategories articlesCategory) {}
    }
}
