using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace NetScratcher
{
    class Program
    {
        static int Main(string[] args)
        {   
            var rootCommand = new RootCommand
            {
                new Option<string>(new[] {"--find","-f"},
                description: "search a specfic subreddit"),

                new Option<string>(new[] {"--term","t"},
                description: "the specific search term (ex, truck, ball"),
                
                new Option  <string>(new[]{"--sort", "s"},
                getDefaultValue:()=>"top",
                description: "Sort posts by relevence hot top new or comments"),
                
                new Option  <int>(new[] {"--max","-m"},
                getDefaultValue:()=>20,
                description: "Set the max number of posts to download"),

            };

            rootCommand.Description = "Reddit search command line tool.";

            rootCommand.Handler = CommandHandler.Create<string,string,string,int>(SearchSub);
            return rootCommand.InvokeAsync(args).Result;
        }

        private static void SearchSub(string find, string term,string sort, int max){
            string appID = ""; //Left blank for git
            string refreshToken = ""; //Left blank for git

            if(find == null){
                Console.WriteLine("arg 'find' required!");
                return;
            }

            if(find == null){
                Console.WriteLine("arg 'find' required!");
                return;
            }

            Search search = new Search();
            search.SearchReddit(appID,refreshToken,find,term,sort,max);
        }
    }
}
