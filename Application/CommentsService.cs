using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using System.Linq;

namespace Application
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository _repository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        public CommentsService(IRepository repository, IArticleRepository articleRepository, IUserRolesRepository userRolesRepository)
        {
            _repository = repository;
            _articleRepository = articleRepository;
            _userRolesRepository = userRolesRepository;
        }

        public bool CanCreateComment(int articleId, int? commentId = null)
        {
            bool comment = true;
            if (commentId.HasValue)
            {
                var comments = _repository.ReadCommentsByAarticlesId(articleId);
                comment = comments.Any(x => x.Id == commentId.Value);
            }
            if (!comment)
                return false;

            var articles = _articleRepository.ReadAllArticles();
            bool article = articles.Any(x => x.Id == articleId);
            if (!article)
                return false;

            return true;
        }

        public void CreateComment(Comments comment)
        {
            _repository.CreateComment(comment);
        }

        public IEnumerable<Comments> ReadLastComments()
        {
            return null;
        }

        public IEnumerable<CommentWithName> ReadArticleComments(int articleId)
        {
            return _repository.ReadCommentsByAarticlesId(articleId);
        }

        public IEnumerable<Comments> ReadAccountComments(int accountId)
        {
            return _repository.ReadCommentsByAccountsId(accountId);
        }

        public void UpdateComment(Comments comment)
        {
        }

        public void DeleteComment(int commentId, int accountId)
        {
            var comments = _repository.ReadAllComments();
            bool isCommentAvailable = comments.Any(x => x.Id == commentId && x.AccountsId == accountId);
            if (isCommentAvailable)
            {
                bool hasReply = comments.Any(x => x.CommentsId == commentId);
                if (hasReply)
                {
                    var deletedComment = new Comments() {
                        Id = commentId,
                        Comment = "[DELETED]",
                        AccountsId = null
                    };

                    _repository.UpdateComment(deletedComment);
                }
                else
                {
                    _repository.DeleteComment(commentId);
                }
            }
        }
    }
}
