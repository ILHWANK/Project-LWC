using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DayRoutine
{
    [HideInInspector] public string dayGroup;
    [HideInInspector] public List<string> routine1List;
    [HideInInspector] public List<string> routine2List;
    [HideInInspector] public List<string> routine3List;
    [HideInInspector] public List<string> routine4List;
    [HideInInspector] public List<string> routine5List;
    [HideInInspector] public List<string> routine6List;
    [HideInInspector] public List<string> routine7List;
    [HideInInspector] public List<string> routine8List;
    [HideInInspector] public List<string> routine9List;
    [HideInInspector] public List<string> routine10List;
}

[Serializable]
public class DayRoutineEvent
{
    public DayRoutine[] DayRoutines;
}

