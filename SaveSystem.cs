using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem 
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/PrefabsTranforms/";
    public static void Init()
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void SaveToFile(string saveFileName, string saveData)
    {
        File.WriteAllText( SAVE_FOLDER + saveFileName + ".json", saveData );
    }

    public static string LoadFromFile(string saveFileName)
    {
        if(File.Exists(SAVE_FOLDER + saveFileName + ".json"))
        {
            string data = File.ReadAllText(SAVE_FOLDER + saveFileName + ".json");
            return data;
        }
        else
        {
            return null;
        }
    }
}
