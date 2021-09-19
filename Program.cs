using System;
using Newtonsoft.Json.Linq;
using System.IO;

namespace NetScratcher
{
    class Program
    {
        static void Main(string[] args)
        {   
            //create a secret.json and store your json values like so
            // "REDDIT_APP_ID" : "appid",
            // "MY_REFRESH_TOKEN" : "refreshtoken"
            string secret = ("secret.json");
            JObject SObj = JObject.Parse(File.ReadAllText(secret));
            string appID = (string)SObj["REDDIT_APP_ID"];
            string refreshToken = (string)SObj["MY_REFRESH_TOKEN"];

            Search search = new Search();

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
