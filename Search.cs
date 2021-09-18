using System;
using Reddit;
using Reddit.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

class Search
{
    public void SearchReddit(string appID, string refreshToken)
    {
        var reddit = new RedditClient(appID,refreshToken);

        Console.WriteLine("Enter a subbreddit");
        string subReddit = Console.ReadLine();
        Console.WriteLine("Enter a search term");
        string searchTerm = Console.ReadLine();

        List<Post> posts = reddit.Subreddit(subReddit).Search(new Reddit.Inputs.Search.SearchGetSearchInput(searchTerm, limit: 100));

        Console.WriteLine($"over {posts.Count} results for {searchTerm} in r/{subReddit} found!");
        foreach (var x in posts){
            Console.WriteLine(x.Title);
        }
    }
}