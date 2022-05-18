using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRepository
    {
        void CreateComment(Comments comment);
        IEnumerable<Comments> ReadAllComments();
        IEnumerable<CommentWithName> ReadCommentsByAarticlesId(int articleId);
        IEnumerable<Comments> ReadCommentsByAccountsId(int accountId);
        void UpdateComment(Comments comment);
        void DeleteComment(int commentId);
    }
}
