using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.Vo
{
    using System.Diagnostics;
    using Util.Define;
    using WHDle.Util;

    [Serializable]
    public class VOItemBase : VOBase
    {
        public ItemType ItemType;
        public int ItemCode;
        public string EnglishName;
        public string KoreanName;
        public string ItemExplain;

        public int ItemAmount;

        protected ItemType GetItemType(string itemId, int last = 1)
        {
            return Enum.Parse<ItemType>(itemId[0..last]);
        }

        protected int GetItemCode(string itemId, int first = 2)
        {
            return int.Parse(itemId[first..]);
        }
    }
}
