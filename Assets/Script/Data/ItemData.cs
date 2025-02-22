using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Data
{
    [System.Serializable]
    public class ItemData
    {
        [HideInInspector] public string itemID;
        [HideInInspector] public int itemCount;
    }
}
