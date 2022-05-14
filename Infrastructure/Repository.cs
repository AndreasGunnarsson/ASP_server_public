using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Dapper;

namespace Infrastructure
{
    public class Repository : IRepository
    {
        private readonly IAppDb _db;

        public Repository(IAppDb db)
        {
            _db = db;
        }

        public void CreateComment(Comments comment)
        {
            string sql = "INSERT INTO Comments (Comment, AccountsId, ArticlesId, CommentsId) VALUES (@CommentText, @AccountId, @ArticleId, @CommentId)";
            var parameters = new {
                CommentText = comment.Comment,
                AccountId = comment.AccountsId,
                ArticleId = comment.ArticlesId,
                CommentId = comment.CommentsId.HasValue ? comment.CommentsId.Value : (int?)null
            };

            _db.Connection.Execute(sql, parameters);
        }

        public IEnumerable<Comments> ReadAllComments()
        {
            return null;
        }

        public IEnumerable<Comments> ReadCommentsByAarticlesId(int articleId)
        {
            string sql = "SELECT * FROM Comments WHERE ArticlesId = @ArticleIdToFilter";
            var parameters = new {
                ArticleIdToFilter = articleId
            };

            var comments = _db.Connection.Query<Comments>(sql, parameters);
            return comments;
        }

        public IEnumerable<Comments> ReadCommentsByAccountsId(Account accountId)
        {
            return null;
        }

        public void UpdateComment(Comments comment)
        {
        }

        public void DeleteComment(Comments comment)
        {
        }
    }
}
