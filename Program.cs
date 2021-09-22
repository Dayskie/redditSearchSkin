using System;

namespace NetScratcher
{
    class Program
    {
        static void Main(string[] args)
        {   
            string appID = ""; //Left blank for git
            string refreshToken = ""; //Left blank for git

            Search search = new Search();

            //todo fix this shit i want to add flags like -f subreddit -s top =c 10
            string sub ="";
            string key ="";
            string sort ="";
            int postLimit = 0;

            try{
                sub         = args[0];
                key         = args[1];
                sort        = args[2]; //relevence hot top new comments
                postLimit   = int.Parse(args[3]);
                    
            }
            catch{
                Console.WriteLine("ERROR incorrect value (string) sub (string) keyword (int) maxPosts");
            }
            search.SearchReddit(appID,refreshToken,sub,key,sort,postLimit);
        }
    }
}
