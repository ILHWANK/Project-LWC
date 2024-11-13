using System.IO;
using UnityEngine;

public static class PlayerDataFileHandler
{
    private static string FilePath => Application.dataPath + "/PlayerDatas/";

    public static void FileSave(PlayerData playerData, string saveFileName)
    {
        if (!Directory.Exists(FilePath))
        {
            Directory.CreateDirectory(FilePath);
        }

        var jsonFile = JsonUtility.ToJson(playerData);
        var saveFilePath = $"{FilePath}{saveFileName}.json";

        File.WriteAllText(saveFilePath, jsonFile);

        PlayerDataManager.UpdateCache(playerData);
    }

    public static PlayerData FileLoad(string saveFileName)
    {
        var saveFilePath = $"{FilePath}{saveFileName}.json";

        if (!File.Exists(saveFilePath))
        {
            return null;
        }

        var jsonFile = File.ReadAllText(saveFilePath);
        var playerData = JsonUtility.FromJson<PlayerData>(jsonFile);

        PlayerDataManager.UpdateCache(playerData);

        return playerData;
    }
}

