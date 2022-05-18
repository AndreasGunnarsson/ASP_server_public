using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods for comments.</summary>
    public interface ICommentsService
    {
        bool CanCreateComment(int articleId, int? commentId = null);
        void CreateComment(Comments comment);
        IEnumerable<Comments> ReadLastComments();
        IEnumerable<CommentWithName> ReadArticleComments(int articleId);
        IEnumerable<Comments> ReadAccountComments(int accountId);
        void UpdateComment(Comments comment);
        void DeleteComment(int commentId, int accountId);
    }
}
