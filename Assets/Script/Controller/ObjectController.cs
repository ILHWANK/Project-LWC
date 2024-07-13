using UnityEngine;

namespace Script.Controller
{
    public class ObjectController : MonoBehaviour
    {
        public enum ObjectType
        {
            None,
            Letter,
            MiniGame1,
            MiniGame2,
            MiniGame3,
            Witch
        }

        public ObjectType _objectType = ObjectType.None;

        public ObjectType GetObjectType()
        {
            return _objectType;
        }
    }
}
