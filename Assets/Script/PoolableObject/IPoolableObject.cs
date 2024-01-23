using System;

namespace WHDle.Util
{
    public interface IPoolableObject
    {
        bool CanRecyle { get; set; }
        Action OnRecyleStart { get; set; }
    }
}
