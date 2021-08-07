using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICreateArticle
    {
        void addArticle(Articles article);
        void updateArticle(Articles article);
        void addCategory(IEnumerable<ArticlesCategories> articlesCategories);       // En lista med articlecategories som skickas in för att lägga till kategorier till en artikel.
    }
}

// För admin i syfte att skapa artiklar.
