using System;
using Reddit;
using Reddit.Controllers;
using System.Collections.Generic;

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
            search.SearchReddit(appID,refreshToken);
        }
    }
}
