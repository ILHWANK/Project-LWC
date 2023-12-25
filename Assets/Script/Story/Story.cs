using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Story
{
    public string characterName;

    public string[] contexts;
}

[System.Serializable]
public class StoryEvent
{
    public string characterName;

    public Vector2 line;
    public Story[] storys;
}
