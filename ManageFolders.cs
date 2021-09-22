using System.IO;
using Newtonsoft.Json.Linq;

public class Folders
{
    public string CreateFolder(string search, string image){
        //extracting export info from settings.json
        string settings = "settings.json";
        JObject SObjs = JObject.Parse(File.ReadAllText(settings));
        bool customOutput = (bool)SObjs["custom-path"];
        string path;

        if(customOutput){
            path = (string)SObjs["export-location"];;
        } else{
            path = System.Environment.CurrentDirectory + "/output/";
        }

        string filterFolder =  path + $"/{search}/";

        if(!Directory.Exists(path+search)){
            Directory.CreateDirectory(filterFolder);
        }

        string[] filesInFolder   = Directory.GetFiles(filterFolder);
        int imageNum = filesInFolder.Length;
        string fileName = search + imageNum;

        //output + $"{search}{imageCount}" + Path.GetExtension(image)
        return filterFolder + fileName + Path.GetExtension(image);
    }
}