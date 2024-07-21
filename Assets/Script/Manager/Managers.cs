using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;

public class Managers : MonoBehaviour, IDisposable
{
    private static TestManager _testManager = new TestManager();
    public static TestManager Test => _testManager;

    private static TestManager2 _testManager2 = new TestManager2();
    public static TestManager2 Test2 => _testManager2;

    private List<IQueingable> _queue;

    private void Awake()
    {
        _queue.Add(Test);
        _queue.Add(Test2);
        
        Init();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void Init()
    {
        foreach (var qu in _queue)
        {
            qu.Init();
        }
    }

    private void Process()
    {
        foreach (var qu in _queue)
        {
            qu.Process();
        }
    }
    
    public void Dispose()
    {
        foreach (var qu in _queue)
        {
            qu.Dispose();
        }
    }
}

internal interface IQueingable
{
    void Init();
    void Dispose();
    void Process();
}
