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

            while (true)
            {
                Console.WriteLine("What would you like to do? Find images or quit? f/q");
                string input = Console.ReadLine();

                if(input == "f"){
                    search.SearchReddit(appID,refreshToken);
                } else if(input == "q"){
                    break;
                } else{
                    Console.WriteLine("Invalid Response!");
                    continue;
                }
            }
        }
    }
}
