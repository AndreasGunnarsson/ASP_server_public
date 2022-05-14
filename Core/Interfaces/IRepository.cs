using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRepository
    {
        void CreateComment(Comments comment);
        IEnumerable<Comments> ReadAllComments();
        IEnumerable<Comments> ReadCommentsByAarticlesId(int articleId);
        IEnumerable<Comments> ReadCommentsByAccountsId(Account accountId);
        void UpdateComment(Comments comment);
        void DeleteComment(Comments comment);
    }
}
