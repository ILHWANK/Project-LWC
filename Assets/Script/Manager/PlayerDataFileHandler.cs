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

        // PlayerDataManager에 캐시 업데이트를 직접 요청하지 않음
    }

    public static PlayerData FileLoad(string saveFileName)
    {
        var saveFilePath = $"{FilePath}{saveFileName}.json";

        if (!File.Exists(saveFilePath))
        {
            return null;
        }

        var jsonFile = File.ReadAllText(saveFilePath);
        return JsonUtility.FromJson<PlayerData>(jsonFile);
    }
}