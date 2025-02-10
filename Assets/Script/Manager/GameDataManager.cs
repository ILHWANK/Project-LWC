using System.Collections.Generic;
using System.IO;
using Script.Core;
using UnityEngine;

namespace Script.Manager
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;
        public GameData GameData = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                LoadGameData();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // 게임 데이터 저장
        public void SaveGameData()
        {
            var json = JsonUtility.ToJson(GameData, true);
            File.WriteAllText(Application.persistentDataPath + "/gameData.json", json);
        }

        // 게임 데이터 로드
        private void LoadGameData()
        {
            var filePath = Application.persistentDataPath + "/gameData.json";
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                GameData = JsonUtility.FromJson<GameData>(json);
                Debug.Log("Game state loaded.");
            }
            else
            {
                GameData = new GameData
                {
                    CurrentDay = 1,
                    CurrentRoutine = "기상",
                    NextRoutine = "아침 식사",
                    WorldAffinity = new Dictionary<string, int> { { "세계1", 0 }, { "세계2", 0 } },
                    Inventory = new Dictionary<string, int>(),
                    MiniGameLevels = new Dictionary<string, int> { { "CookingGame", 1 }, { "FishingGame", 1 } }
                };
            }
        }

        // 아이템 추가
        public void AddItem(string itemId, int count)
        {
            if (GameData.Inventory.ContainsKey(itemId))
            {
                GameData.Inventory[itemId] += count;
            }
            else
            {
                GameData.Inventory[itemId] = count;
            }

            SaveGameData();
        }

        // 아이템 삭제
        public void RemoveItem(string itemId)
        {
            if (GameData.Inventory.ContainsKey(itemId))
            {
                GameData.Inventory.Remove(itemId);
                SaveGameData();
                Debug.Log($"Item {itemId} removed.");
            }
            else
            {
                Debug.LogWarning($"Item {itemId} not found.");
            }
        }

        // 미니게임 난이도 설정
        public void SetMiniGameLevel(string gameName, int level)
        {
            if (GameData.MiniGameLevels.ContainsKey(gameName))
            {
                GameData.MiniGameLevels[gameName] = level;
            }
            else
            {
                GameData.MiniGameLevels.Add(gameName, level);
            }

            SaveGameData();
        }

        // 일과 관리
        public void UpdateRoutines(string current, string next)
        {
            GameData.CurrentRoutine = current;
            GameData.NextRoutine = next;
            SaveGameData();
        }

        public string GetCurrentRoutine() => GameData.CurrentRoutine;
        public string GetNextRoutine() => GameData.NextRoutine;
    }
}