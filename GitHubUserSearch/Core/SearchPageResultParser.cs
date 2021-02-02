using GitHubUserSearch.Core.Interfaces;
using System.Collections.Generic;

namespace GitHubUserSearch.Core
{
    public class SearchPageResultParser
    {
        private ISearchPageResultParser _searchPageResultParser;
        public SearchPageResultParser(ISearchPageResultParser searchPageResultParser)
        {
            _searchPageResultParser = searchPageResultParser;
        }
        public string GetHtml(string pageNumberLink = null)
        {
            return _searchPageResultParser.GetHtml(pageNumberLink);
        }
        public List<string> GetUsernames(List<string> htmlWordList)
        {
            return _searchPageResultParser.GetUsernames(htmlWordList);
        }
        public int GetTotalPages(List<string> htmlWordList)
        {
            return _searchPageResultParser.GetTotalPages(htmlWordList);
        }
    }
}