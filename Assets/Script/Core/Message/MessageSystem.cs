using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem
{
    private static MessageSystem _instance;
    public static MessageSystem Instance => _instance ??= new MessageSystem();

    private readonly Dictionary<string, Delegate> _messageHandlers = new();

    public void Subscribe<T>(string message, Action<T> callback)
    {
        if (!_messageHandlers.ContainsKey(message))
            _messageHandlers[message] = callback;
        else
            _messageHandlers[message] = Delegate.Combine(_messageHandlers[message], callback);
    }

    public void Unsubscribe<T>(string message, Action<T> callback)
    {
        if (_messageHandlers.ContainsKey(message))
        {
            _messageHandlers[message] = Delegate.Remove(_messageHandlers[message], callback);
            if (_messageHandlers[message] == null)
                _messageHandlers.Remove(message);
        }
    }

    public void Publish<T>(string message, T data)
    {
        if (_messageHandlers.TryGetValue(message, out var callback))
        {
            if (callback is Action<T> action)
                action.Invoke(data);
            else
                Debug.LogError($"Message {message} has incorrect type {typeof(T)}");
        }
    }
}