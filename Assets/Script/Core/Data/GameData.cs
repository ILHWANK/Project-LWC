using System.Collections.Generic;

namespace Script.Core
{
    public class GameData
    {
        public int CurrentDay;                       // 현재 몇 일차인지
        public string CurrentRoutine;                // 현재 일과
        public string NextRoutine;                   // 다음 일과
        public Dictionary<string, int> WorldAffinity; // 각 세계관별 호감도
        public Dictionary<string, int> Inventory;     // ItemId와 보유 수량
        public Dictionary<string, int> MiniGameLevels; // 미니게임 난이도 ("CookingGame" -> 2)
    }   
}