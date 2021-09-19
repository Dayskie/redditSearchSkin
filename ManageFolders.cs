using System;
using System.IO;

public class Folders
{
    public string defaultPath = System.Environment.CurrentDirectory + "/output/";
    public string CreateFolder(string search,int imageCount, string image){
        string outputDestination = defaultPath;
        string filterFolder =  defaultPath + $"/{search}/";

        if(!Directory.Exists(defaultPath+search)){
            Directory.CreateDirectory(filterFolder);
        }
        
        outputDestination = filterFolder + search + imageCount;

        //output + $"{search}{imageCount}" + Path.GetExtension(image)
        return outputDestination + Path.GetExtension(image);
    }
}