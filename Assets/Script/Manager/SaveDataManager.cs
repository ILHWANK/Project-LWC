using System.IO;
using System.Text;
using UnityEngine;

public static class SaveDataManager
{
    private static string FilePath => Application.dataPath + "/PlayerDatas/";

    public static void FileSave(PlayerData playerData, string saveFileName)
    {
        if (!Directory.Exists(FilePath))
        {
            Directory.CreateDirectory(FilePath);
        }

        var jsonFile = JsonUtility.ToJson(playerData);

        var stringBilder = new StringBuilder();

        stringBilder.Append(FilePath);
        stringBilder.Append(saveFileName);
        stringBilder.Append(".json");

        var saveFilePath = stringBilder.ToString();

        Debug.Log(saveFilePath);

        File.WriteAllText(saveFilePath, jsonFile);
    }

    public static PlayerData FileLoad(string saveFileName)
    {
        var stringBilder = new StringBuilder();

        stringBilder.Append(FilePath);
        stringBilder.Append(saveFileName);
        stringBilder.Append(".json");

        var saveFilePath = stringBilder.ToString();

        if (!File.Exists(saveFilePath))
        {
            return null;
        }

        var jsonFile = File.ReadAllText(saveFilePath);

        var playerData = JsonUtility.FromJson<PlayerData>(jsonFile);

        return playerData;
    }
}
