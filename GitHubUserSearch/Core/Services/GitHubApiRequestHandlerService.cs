using GitHubUserSearch.Core.Interfaces;
using GitHubUserSearch.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubUserSearch.Core
{
    public class GitHubRequestHandlerService : IGitHubRequestHandler
    {
        public async Task<List<UserModel>> GetUserDataAsync(List<string> usernames)
        {
            var users = new List<UserModel>();
            for (var i = 0; i < usernames.Count(); i++)
            {
                var username = usernames.ElementAt(i);
                var client = new RestClient("https://api.github.com/users/" + username)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer 3b3024abb64e6961017b08a1d218b1bb13bab679");
                request.AddHeader("Cookie", "_octo=GH1.1.205917196.1612033113; logged_in=no");
                var response = await client.ExecuteAsync(request);
                var user = JsonConvert.DeserializeObject<UserModel>(response.Content);
                users.Add(user);
            }
            return users;
        }
    }
}