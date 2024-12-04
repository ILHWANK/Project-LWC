using UnityEngine;

[System.Serializable]
public class Item
{
    [HideInInspector] public string id;
    [HideInInspector] public string name;
    [HideInInspector] public string desc;
    [HideInInspector] public string subDesc;
    [HideInInspector] public string path;
    
    [HideInInspector] public string type;
}
