using GitHubUserSearch.Core.Interfaces;
using GitHubUserSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubUserSearch.Controllers
{
    //Build UI
    [ApiController]
    [Route("api/user")]
    public class UserSearchController : Controller
    {
        private ISearchPageResultParser _searchPageResultParser;
        private IGitHubRequestHandler _gitHubRequestHandler;
        public UserSearchController(ISearchPageResultParser searchPageResultParser, IGitHubRequestHandler gitHubRequestHandler)
        {
            _searchPageResultParser = searchPageResultParser;
            _gitHubRequestHandler = gitHubRequestHandler;
        }

        //[HttpGet]
        //[Route("username")]
        //public async Task<string> GetUsersByUsername(string username)
        //{
        //    var searchResult = await _gitHubRequestHandler.GetUserDataAsync(new List<string>() { username });
        //    return JsonConvert.SerializeObject(searchResult);
        //}

        [HttpGet]
        [Route("name/{name}/page/{page}")]
        public async Task<string> GetUsersByNameAsync(string name, int page)
        {
            var nameParts = string.Join("+", name.Split(" "));
            var link = "https://github.com/search?q=" + nameParts + "&type=users";
            var html = _searchPageResultParser.GetHtml(link + "&p=" + page).Split(" ").ToList();
            var totalPagination = _searchPageResultParser.GetTotalPages(html);
            var usernames = _searchPageResultParser.GetUsernames(html);
            var users = await _gitHubRequestHandler.GetUserDataAsync(usernames);
            var searchResult = new SearchResultModel()
            {
                Users = users,
                TotalPageNumbers = totalPagination,
                CurrentPageNumber = page
            };
            return JsonConvert.SerializeObject(searchResult);
        }
    }
}