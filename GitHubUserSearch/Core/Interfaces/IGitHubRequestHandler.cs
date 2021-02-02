using GitHubUserSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubUserSearch.Core.Interfaces
{
    public interface IGitHubRequestHandler
    {
        public Task<List<UserModel>> GetUserDataAsync(List<string> usernames);
    }
}
