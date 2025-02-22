using System;
using UnityEngine;

public enum LetterType
{
    None,
}

[Serializable]
public class Letter
{
    [HideInInspector] public string id;
    [HideInInspector] public string world;
    [HideInInspector] public string from;
    [HideInInspector] public string info;
    [HideInInspector] public LetterType letterType;
    [HideInInspector] public string dialogueGroup;
}
