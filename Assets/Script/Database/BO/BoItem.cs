using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Database.BO
{
    using SD;

    [Serializable]
    public class BoItem : BoBase
    {
        public SDItem SDItem;
        public int Amount;
    }
}
