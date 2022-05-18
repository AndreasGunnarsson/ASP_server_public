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
            string sql = "SELECT * FROM Comments";
            return _db.Connection.Query<Comments>(sql);
        }

        public IEnumerable<CommentWithName> ReadCommentsByAarticlesId(int articleId)
        {
            string sql = "SELECT Comments.Id, Comment, CreateDate, EditDate, AccountsId, CommentsId, Name FROM Comments LEFT JOIN Accounts ON Comments.AccountsId = Accounts.Id WHERE ArticlesId = @ArticleIdToFilter;";
            var parameters = new {
                ArticleIdToFilter = articleId
            };

            return _db.Connection.Query<CommentWithName>(sql, parameters);
        }

        public IEnumerable<Comments> ReadCommentsByAccountsId(int accountId)
        {
            string sql = "SELECT * FROM Comments WHERE AccountsId = @SelectedAccountId";
            var parameters = new {
                SelectedAccountId = accountId
            };

            return _db.Connection.Query<Comments>(sql, parameters);
        }

        public void UpdateComment(Comments comment)
        {
            string sql = "UPDATE Comments SET Comment = @CommentText, AccountsId = @AccountId, EditDate = CURRENT_TIMESTAMP WHERE Id = @IdToChange";
            var parameters = new {
                IdToChange = comment.Id,
                CommentText = comment.Comment,
                AccountId = comment.AccountsId,
            };

            _db.Connection.Execute(sql, parameters);
        }

        public void DeleteComment(int commentId)
        {
            string sql = "DELETE FROM Comments WHERE Id = @IdToDelete";
            var parameters = new {
                IdToDelete = commentId
            };

            _db.Connection.Execute(sql, parameters);
        }
    }
}
