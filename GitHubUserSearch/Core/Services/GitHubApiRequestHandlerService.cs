using GitHubUserSearch.Core.Interfaces;
using GitHubUserSearch.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserSearch.Core
{
    public class GitHubRequestHandlerService : IGitHubRequestHandler
    {
        public async Task<List<UserModel>> GetUserDataAsync(List<string> usernames)
        {
            Console.WriteLine(string.Join(',', usernames));
            var users = new List<UserModel>();
            for (var i = 0; i < usernames.Count(); i++)
            {
                var username = usernames.ElementAt(i);
                var client = new RestClient("https://api.github.com/users/" + username)
                {
                    Timeout = -1
                };
                var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("nro111:ba3fd14bfb19090816382394a17dce8cbd88ada6"));
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Basic {base64authorization}");
                var response = await client.ExecuteAsync(request);
                var user = JsonConvert.DeserializeObject<UserModel>(response.Content);
                Console.WriteLine(user.login);
                users.Add(user);
            }
            return users;
        }
    }
}