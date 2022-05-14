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

        public CommentsService(IRepository repository, IArticleRepository articleRepository)
        {
            _repository = repository;
            _articleRepository = articleRepository;
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

        public IEnumerable<Comments> ReadArticleComments(int articleId)
        {
            return _repository.ReadCommentsByAarticlesId(articleId);
        }

        public IEnumerable<Comments> ReadAccountComments(Account accountId)
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
