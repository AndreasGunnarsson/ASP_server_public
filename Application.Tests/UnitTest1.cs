using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Core.Interfaces;
using Core.Entities;
using System.Collections.Generic;
using System;

namespace Application.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [DataRow(1, null, true)]
    [DataRow(1, 1, true)]
    [DataRow(2, null, false)]
    public void ReturnBoolIfAccountCanCreateCommentToArticle(int articleId, int? commentId, bool expected)
    {
        // Arrange
        IEnumerable<Articles> articles = new Articles[] {
            new Articles { Id = 1, Title = "TestTitle1", CreateDate = DateTime.Now, EditDate = DateTime.Now }
        };
        IEnumerable<CommentWithName> comments = new CommentWithName[] {
            new CommentWithName { Id = 1, Comment = "TestComment", CreateDate = DateTime.Now, EditDate = DateTime.Now, AccountsId = 1, CommentsId = 1, Name = "TestName" }
        };

        var repoMock = new Mock<IRepository>();
        repoMock.Setup(x => x.ReadCommentsByAarticlesId(articleId)).Returns(comments);
        var articleRepoMock = new Mock<IArticleRepository>();
        articleRepoMock.Setup(x => x.ReadAllArticles()).Returns(articles);
        var userRoleRepoMock = new Mock<IUserRolesRepository>();
        var commentSerivce = new CommentsService(repoMock.Object, articleRepoMock.Object, userRoleRepoMock.Object);

        // Act
        var isTrue = commentSerivce.CanCreateComment(articleId);

        // Assert
        Assert.AreEqual(expected, isTrue);;
    }
}
