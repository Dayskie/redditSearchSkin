using System;

namespace NetScratcher
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var appID           = Environment.GetEnvironmentVariable("REDDIT_APP_ID");
            var refreshToken    = Environment.GetEnvironmentVariable("MY_REFRESH_TOKEN");
            Search search = new Search();

            string sub ="";
            string key ="";
            string sort ="";
            int postLimit = 0;

            try{
                sub = args[0];
                key = args[1];
                sort = args[2]; //relevence hot top new comments
                postLimit = int.Parse(args[3]);
                    
            }
            catch{
                Console.WriteLine("ERROR incorrect value (string) sub (string) keyword (int) maxPosts");
            }
            search.SearchReddit(appID,refreshToken,sub,key,sort,postLimit);
        }
    }
}
