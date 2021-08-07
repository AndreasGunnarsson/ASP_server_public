using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure
{
    public class Repository : IRepository
    {
        public void CreateCategory(Categories category) {}
        public IEnumerable<Categories> ReadCategory()
        {
            return null;
        }
        public void UpdateCategory(Categories category) {}
        public void DeleteCategory(Categories category) {}

        // Articles:
        public void CreateArticle(Articles article) {}
        public IEnumerable<Articles> ReadAllArticles()
        {
            return null;
        }
        public IEnumerable<int> ReadCategoriesByArticlesId(int articlesId)
        {
            return null;
        }
        // ^TODO: Måste ha egen entitet (istället för int).^ Kanske inte behövs? Kanske är bättre att användsa ReadCategory (se ovan) och sortera i efterhand?
        public void UpdateArticle(Articles article) {}
        public void CreateArticlesCategory(ArticlesCategories articleCategory) {}
        public void RemoveArticlesCategory(ArticlesCategories articlesCategory) {}

        // Comments:
        public void CreateComment(Comments comment) {}
        public IEnumerable<Comments> ReadAllComments()
        {
            return null;
        }
        public IEnumerable<Comments> ReadCommentsByAarticlesId(Articles articleId)
        {
            return null;
        }
        public IEnumerable<Comments> ReadCommentsByAccountsId(Accounts accountId)
        {
            return null;
        }
        public void UpdateComment(Comments comment) {}
        public void DeleteComment(Comments comment) {}
    }
}

// Implementera IRepository-interfacet här!
