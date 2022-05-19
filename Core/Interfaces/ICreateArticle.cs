using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICreateArticleService
    {
        IEnumerable<Articles> ReadAllArticles();
        string ReadArticleFile(int id);
        void CreateArticle(ArticleTransfer article);
        void UpdateArticle(ArticleTransfer article);
        void AddCategory(IEnumerable<ArticlesCategories> articlesCategories);
    }
}
