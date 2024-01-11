using System;
using System.Collections;
using System.Collections.Generic;

namespace WHDle.Database.SD
{
    using Util.Define;

    [Serializable]
    public class SDItem : StaticBase
    {
        public int ItemIndex;
        public string ItemId;
        public int ItemNameRef;
        public ItemType ItemType;
        public SubType SubType;
        public int ItemExplainRef;
        public int ItemMaxAmountRef;
    }
}