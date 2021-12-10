using System.IO;
using System.Xml.Serialization;
using UnityEngine;
public class GameProcess
{
    public int LevelSuccess { get; set; }
    public int Coins { get; set; }
    public string Name { get; set; }

    public GameProcess()
    {
        LevelSuccess = 0;
        Coins = 0;
        Name = "noName";
    }
    private static readonly string SavePath = @$"{GlobalConfig.rootPath}\gameProcess.xml";
    public static bool IsHas
    {
        get
        {
            return File.Exists(SavePath); ;
        }
    }
    public static void SaveProcess(GameProcess gameProcess)
    {
        XmlSerializer formatter = new XmlSerializer(typeof(GameProcess));
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
        using FileStream fs = new FileStream(SavePath, FileMode.OpenOrCreate);
        formatter.Serialize(fs, gameProcess);
    }
    public static GameProcess LoadProcess()
    {
        XmlSerializer formatter = new XmlSerializer(typeof(GameProcess));
        GameProcess GameProcess;
        using (FileStream fs = new FileStream(SavePath, FileMode.OpenOrCreate))
        {
            GameProcess = (GameProcess)formatter.Deserialize(fs);
        }
        return GameProcess;
    }
    public void Save()
    {
        SaveProcess(this);
    }
    public void LevelSuccessful(int num)
    {
        if (LevelSuccess < num)
        {
            LevelSuccess = num;
            Save();
        }
    }
}
