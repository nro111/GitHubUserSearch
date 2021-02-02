using GitHubUserSearch.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace GitHubUserSearch.Core
{
    public class SearchPageResultParserService : ISearchPageResultParser
    {
        public string GetHtml(string pageNumberLink = null)
        {
            if (string.IsNullOrEmpty(pageNumberLink))
                pageNumberLink = "https://github.com/search?q=john+doe&type=users";
            var html = string.Empty;
            using (var webClient = new WebClient())
            {
                html = webClient.DownloadString(pageNumberLink);
            }
            return html;
        }
        public List<string> GetUsernames(List<string> htmlWordList)
        {
            return htmlWordList
                    .FindAll(word => word.Contains("data-hovercard-url=\"/users/"))
                    .Select(line => line.Remove(0, "data-hovercard-url=\"/users/".Length))
                    .Select(line => line.Remove(line.IndexOf("/"), 11))
                    .ToList();
        }
        public int GetTotalPages(List<string> htmlWordList)
        {
            var pages = htmlWordList
                       .FindAll(word => word.Contains("?p="));
            if (pages.Count() > 0)
                return htmlWordList
                    .FindAll(word => word.Contains("?p="))
                    .Select(x => Regex.Match(x, @"\d+").Value)
                    .Distinct()
                    .Select(number => int.Parse(number))
                    .ToList()
                    .Last();
            else
                return 0;
        }
    }
}