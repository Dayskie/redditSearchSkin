using System;
using Reddit;
using Reddit.Controllers;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

class Search
{
    public void SearchReddit(string appID, string refreshToken, string sub, string search,string time, int maxPosts)
    {
        var reddit                          = new RedditClient(appID,refreshToken);
        Folders folders                     = new Folders();
        WebClient webClient                 = new WebClient();
        System.Diagnostics.Stopwatch timer  = new System.Diagnostics.Stopwatch();

        try {
            List<Post> posts = reddit.Subreddit(sub).Search(new Reddit.Inputs.Search.SearchGetSearchInput
            (search,sort:time,type:"link", limit: maxPosts));
            int imageCount = 0;

            Console.WriteLine($"over {posts.Count} results for {search} in r/{sub} found!");
            timer.Start();

            foreach (var x in posts){
                Console.WriteLine("Downloading");

                string link = webClient.DownloadString("https://www.reddit.com" + x.Permalink + ".json");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Image downloaded");
                Console.ResetColor();

                dynamic dObj = JsonConvert.DeserializeObject<dynamic>(link);

                string image = (string)dObj[0]["data"]["children"][0]["data"]["url"];

                if(Path.GetExtension(image) == ".png" || Path.GetExtension(image) == ".jpg" || Path.GetExtension(image) == ".gif")
                {       
                    webClient.DownloadFile(image,folders.CreateFolder(search,imageCount,image));
                    imageCount++;
                    
                    Console.WriteLine(image);
                } else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unable to format image (unsuported format)");
                    Console.ResetColor();
                }
            }

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;

            Console.WriteLine($"Finished in {timeTaken} with {imageCount} images collected!");
            imageCount = 0;
            timer.Reset();
        }
        catch {
             Console.WriteLine("UNKOWN ERROR (possible 404)");
        }
    }
}