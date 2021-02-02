using System.Collections.Generic;

namespace GitHubUserSearch.Core.Interfaces
{
    public interface ISearchPageResultParser
    {
        public string GetHtml(string pageNumberLink);
        public List<string> GetUsernames(List<string> htmlWordList);
        public int GetTotalPages(List<string> htmlWordList);
    }
}
