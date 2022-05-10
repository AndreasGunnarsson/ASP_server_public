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

        public void CreateComment(Comments comment) {}
        public IEnumerable<Comments> ReadAllComments()
        {
            return null;
        }
        public IEnumerable<Comments> ReadCommentsByAarticlesId(Articles articleId)
        {
            return null;
        }
        public IEnumerable<Comments> ReadCommentsByAccountsId(Account accountId)
        {
            return null;
        }
        public void UpdateComment(Comments comment) {}
        public void DeleteComment(Comments comment) {}
    }
}
