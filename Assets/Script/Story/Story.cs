using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    None,
    FadeOut,
    FadeIn,
    FlashOut,
    FlashIn
}

[System.Serializable]
public class Story
{
    public CameraType cameraType;

    [HideInInspector]
    public string characterName;

    [HideInInspector]
    public string[] contexts, spriteNames;

    [HideInInspector]
    public CameraType[] cameraActions;
}

[System.Serializable]
public class StoryEvent
{
    public string characterName;

    public Vector2 line;
    public Story[] storys;
}
