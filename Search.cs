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
        int imageCount = 0;

        Console.WriteLine("Enter a subbreddit");
        string subReddit = Console.ReadLine();
        Console.WriteLine("Enter a search term");
        string searchTerm = Console.ReadLine();
        
        Console.WriteLine("How many posts would you like to see? (max 100)");
        int maxPosts = int.Parse(Console.ReadLine());

        try
        {
            List<Post> posts = reddit.Subreddit(subReddit).Search(new Reddit.Inputs.Search.SearchGetSearchInput(searchTerm,t:"all",type:"link", limit: maxPosts));
            
            WebClient webClient = new WebClient();
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        
            Console.WriteLine($"over {posts.Count} results for {searchTerm} in r/{subReddit} found!");
            timer.Start();

            foreach (var x in posts){
                string link = webClient.DownloadString("https://www.reddit.com" + x.Permalink + ".json");
                dynamic dObj = JsonConvert.DeserializeObject<dynamic>(link);
                //if((bool)dObj[0]["data"]["children"][0]["data"]["is_self"] == false){

                string image = (string)dObj[0]["data"]["children"][0]["data"]["url"];
                if(Path.GetExtension(image) == ".png" || Path.GetExtension(image) == ".jpg")
                {
                    webClient.DownloadFile(image,output + $"{searchTerm}{imageCount}" + Path.GetExtension(image));
                }

                imageCount++;
                    
                Console.WriteLine(image);
                //}
            }

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;

            Console.WriteLine($"Finished in {timeTaken} with {imageCount} images collected!");
            imageCount = 0;
            timer.Reset();
        }
        catch
        {
            Console.WriteLine("UNKOWN ERROR (possible 404)");
        }
    }
}