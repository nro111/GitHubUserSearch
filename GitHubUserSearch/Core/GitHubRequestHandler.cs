using GitHubUserSearch.Core.Interfaces;
using GitHubUserSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubUserSearch.Core
{
    public class GitHubRequestHandler
    {
        private IGitHubRequestHandler _gitHubRequestHandler;
        public GitHubRequestHandler(IGitHubRequestHandler gitHubRequestHandler)
        {
            _gitHubRequestHandler = gitHubRequestHandler;
        }
        public async Task<List<UserModel>> GetUsersAsync(List<string> usernames)
        {
            return await _gitHubRequestHandler.GetUserDataAsync(usernames);
        }
    }
}