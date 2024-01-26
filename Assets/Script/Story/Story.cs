using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Story
{
    [HideInInspector]
    public string characterName;

    [HideInInspector]
    public string[] contexts, spriteName;
}

[System.Serializable]
public class StoryEvent
{
    public string characterName;

    public Vector2 line;
    public Story[] storys;
}
