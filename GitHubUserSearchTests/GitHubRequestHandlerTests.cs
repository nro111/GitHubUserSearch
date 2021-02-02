using GitHubUserSearch.Core;
using GitHubUserSearch.Core.Interfaces;
using GitHubUserSearch.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GitHubUserSearchTests
{
    [TestFixture]
    public class GitHubRequestHandlerTests
    {
        private Mock<IGitHubRequestHandler> MockGitHubRequestHandler;
        private GitHubRequestHandler gitHubRequestHandler;
        private readonly object[] _usernameLists =
        {
            new object[] {new List<string> { "" }},
            new object[] {new List<string> { "nro111" }},
            new object[] {new List<string> { "nro111", "l", "code", "john smith" }},
            new object[] {new List<string> { "n", "" }}
        };

        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource("_usernameLists")]
        public void GetUserDataAsync_Always_ReturnsExpectedResults(List<string> usernames, List<UserModel> expectedUsers)
        {
            MockGitHubRequestHandler = new Mock<IGitHubRequestHandler>(MockBehavior.Strict);
            //MockGitHubRequestHandler.Setup(x => x.GetUserDataAsync(usernames)).Returns(expectedUsers);
            gitHubRequestHandler = new GitHubRequestHandler(MockGitHubRequestHandler.Object);
            var result = gitHubRequestHandler.GetUsersAsync(usernames);
            Assert.That(result, Is.EqualTo(expectedUsers));
            MockGitHubRequestHandler.VerifyAll();
        }
    }
}