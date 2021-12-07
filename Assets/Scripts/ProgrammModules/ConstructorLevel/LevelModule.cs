using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class LevelModule
{
    public int Id { get; }
    public string Name { get; }
    public List<DetailLevelModule> Details;
    public LevelModule()
    {
        Id = 0;
        Name = "New Level";
        Details = new List<DetailLevelModule>();
    }
    public LevelModule(int id, string name, List<DetailLevelModule> details)
    {
        Id = id;
        Name = name;
        Details = details;
    }


    private static readonly string SavePath = @"C:\DFG\Saves\";
    public static void SaveLevel(LevelModule levelModule)
    {
        string savePath = SavePath + levelModule.Name + ".xml";
        XmlSerializer formatter = new XmlSerializer(typeof(LevelModule));
        if (File.Exists(savePath + levelModule.Name + ".xml"))
        {
            File.Delete(savePath);
        }
        using FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate);
        formatter.Serialize(fs, levelModule);
    }
    public static LevelModule LoadLevel(string levelName)
    {
        string savePath = levelName;

        XmlSerializer formatter = new XmlSerializer(typeof(LevelModule));
        LevelModule LevelModule;
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            LevelModule = (LevelModule)formatter.Deserialize(fs);
        }
        return LevelModule;
    }
    public static List<string> GetLevels()
    {
        List<string> LevelNames = new List<string>();
        foreach (string item in Directory.GetFiles(SavePath))
        {
            LevelNames.Add(item);
        }
        return LevelNames;
    }

}
