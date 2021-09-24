using System.IO;
using Newtonsoft.Json.Linq;
using System;

public class Folders
{
    public string CreateFolder(string search, string image){
        //extracting export info from settings.json
        string settings = "settings.json";
        dynamic SObjs = JObject.Parse(File.ReadAllText(settings));
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

    public void CustomPath(string path){
        string settings = "settings.json";
        dynamic jObj = JObject.Parse(File.ReadAllText(settings));

        if(path == "default"){
            jObj["custom-path"] = false;
            Console.WriteLine($"path set to: default");
        } else{
            string fixPath = path.Replace(@"\", "/");
            jObj["export-location"] = fixPath;
            jObj["custom-path"] = true;

            Console.WriteLine($"path {path} created!");
        }

        File.WriteAllText(settings, Newtonsoft.Json.JsonConvert.SerializeObject(jObj, Newtonsoft.Json.Formatting.Indented));
    }
}