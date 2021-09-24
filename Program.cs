using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace NetScratcher
{
    static class Program
    {
        static int Main(string[] args)
        {   
            //sorry for the ugly main file
            Folders folders = new Folders();

            var command = new RootCommand();
            command.Name = "redsearch";
            command.Description ="hi observer";

            var searchCommand = new Command("search"){
                new Option<string>(new[] {"--find","-f"},
                description: "search a specfic subreddit [Required]")
                {
                    IsRequired = true
                },
                new Option<string>(new[] {"--term","-t"},
                description: "the specific search term (ex, truck, ball) [Required]")
                {
                    IsRequired = true
                },

                new Option  <string>(new[]{"--sort", "-s"},
                getDefaultValue:()=>"top",
                description: "Sort posts by relevence hot top new or comments"),

                new Option  <int>(new[] {"--max","-m"},
                getDefaultValue:()=>20,
                description: "Set the max number of posts to download"),
            };

            searchCommand.Description = "Search reddit for images/gifs";
            searchCommand.Handler = CommandHandler.Create<string,string,string,int>(SearchSub);

            command.Add(searchCommand);

            var outputCommand = new Command("output")
            {
                new Option<string>(new[] {"--path","-p"},
                description: "Specify where to output")
                {
                    IsRequired = true
                }
            };
            outputCommand.Description = "Set export location of files, type 'default' to reset location";
            outputCommand.Handler = CommandHandler.Create<string>(folders.CustomPath);

            command.Add(outputCommand);

            return command.Invoke(args);
        }

        //i dont know why but it only returns values with this
        private static void SearchSub(string find, string term,string sort, int max){
            Search search = new Search();
            search.SearchReddit(find,term,sort,max);
        }
    }
}
