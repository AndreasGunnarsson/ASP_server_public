using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IArticleRepository
    {
        void CreateCategory(Categories category);
        IEnumerable<Categories> ReadCategory();
        void UpdateCategory(Categories category);
        void DeleteCategory(Categories category);

        ulong CreateArticle(Articles article);
        IEnumerable<Articles> ReadAllArticles();
        IEnumerable<int> ReadCategoriesByArticlesId(int articlesId);
        void UpdateArticle(Articles article);
        void CreateArticlesCategory(ArticlesCategories articleCategory);
        void RemoveArticlesCategory(ArticlesCategories articlesCategory);
    }
}
