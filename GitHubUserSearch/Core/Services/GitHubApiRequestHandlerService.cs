using GitHubUserSearch.Core.Interfaces;
using GitHubUserSearch.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
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
                request.AddHeader("Authorization", "Bearer ba3fd14bfb19090816382394a17dce8cbd88ada6");
                request.AddHeader("Cookie", "_octo=GH1.1.205917196.1612033113; logged_in=no");
                var response = await client.ExecuteAsync(request);
                Console.WriteLine("response content: " + response.Content);
                var user = JsonConvert.DeserializeObject<UserModel>(response.Content);
                users.Add(user);
            }
            return users;
        }
    }
}