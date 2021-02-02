using System.Collections.Generic;

namespace GitHubUserSearch.Models
{
    public class SearchResultModel
    {
        public List<UserModel> Users { get; set; } = new List<UserModel>();
        public int TotalPageNumbers { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
