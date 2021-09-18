using System;
using Reddit;
using Reddit.Controllers;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

class Search
{
    public void SearchReddit(string appID, string refreshToken)
    {
        var reddit = new RedditClient(appID,refreshToken);

        string output = Directory.GetCurrentDirectory() + @"\output\";

        Console.WriteLine("Enter a subbreddit");
        string subReddit = Console.ReadLine();
        Console.WriteLine("Enter a search term");
        string searchTerm = Console.ReadLine();

        //int maxPosts = int.Parse(Console.ReadLine());

        List<Post> posts = reddit.Subreddit(subReddit).Search(new Reddit.Inputs.Search.SearchGetSearchInput(searchTerm, limit: 1));

        WebClient webClient = new WebClient();
        

        Console.WriteLine($"over {posts.Count} results for {searchTerm} in r/{subReddit} found!");
        foreach (var x in posts){
            string link = webClient.DownloadString("https://www.reddit.com" + x.Permalink + ".json");

            dynamic dObj = JsonConvert.DeserializeObject<dynamic>(link);

            string image = (string)dObj[0]["data"]["children"][0]["data"]["url"];
            Console.WriteLine(image);
            //Console.WriteLine("https://www.reddit.com" + x.Permalink + ".json");

        }

        Console.ReadLine();
    }
}