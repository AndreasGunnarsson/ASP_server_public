/* using System; */
using System.Collections.Generic;
using Core.Entities;
/* using System.Collections.Concurrent; */

namespace Core.Interfaces
{
    public interface IRepository
    {
        // Category manipulation - admin only:
        void CreateCategory(Categories category);
        IEnumerable<Categories> ReadCategory();
        void UpdateCategory(Categories category);
        void DeleteCategory(Categories category);

        // Articles:
        void CreateArticle(Articles article);                                   // Används av admin för att skapa en artikel.
        IEnumerable<Articles> ReadAllArticles();                                // För att kunna lista alla artiklar som finns.
        IEnumerable<int> ReadCategoriesByArticlesId(int articlesId);            // Används av användaren för att kunna lista alla kategorier som hör till en artikel.
        // ^TODO: Måste ha egen entitet (istället för int).^ Kanske inte behövs? Kanske är bättre att användsa ReadCategory (se ovan) och sortera i efterhand?
        void UpdateArticle(Articles article);                                   // Används av admin för att uppdatera en artikel.
        void CreateArticlesCategory(ArticlesCategories articleCategory);        // För att lägga till en katagori till en artikel.
        void RemoveArticlesCategory(ArticlesCategories articlesCategory);       // För att ta bort en kategori från en artikel.

        // Comments:
        void CreateComment(Comments comment);
        IEnumerable<Comments> ReadAllComments();                                // Hämtar ut alla kommentarer. För admin.
        IEnumerable<Comments> ReadCommentsByAarticlesId(Articles articleId);    // Kommentarer för en specifik artikel; används när man ska ladda in en artikel och få dess tillhörande kommentarer.
        IEnumerable<Comments> ReadCommentsByAccountsId(Accounts accountId);     // Hämtar ut alla kommentarer för en specifik användare.
        void UpdateComment(Comments comment);
        void DeleteComment(Comments comment);
    }
}

// TODO: Hade varit snyggare med en generisk metod för read?
    // Kanske går att göra det mesta generiskt genom att ha ett interface som är generiskt och så har man ett extra IRepository för entieter som kräver specialbehandling..
// TODO: ConnectionString här?
// TODO: Speciell entitet för att Accounts som inte skickar med password när man bara ska authentisera och hämta ut alla konton?
// TODO: Dela upp i flera små repositories utever category, articles, comments och account?
// Behövs "Unit of work"?
    // Om man gör "hela" queries (alla UPDATE och INSERT i en query för en sida) borde det inte behövas?
    // https://en.wikipedia.org/wiki/Database_transaction
    // CQRS istället för Unit of work?
