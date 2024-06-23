using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public enum ObjectType
    {
        None,
        Letter,
        MiniGame1,
        MiniGame2,
        MiniGame3
    }

    public ObjectType _objectType = ObjectType.None;

    public ObjectType GetObjectType()
    {
        return _objectType;
    }
}
