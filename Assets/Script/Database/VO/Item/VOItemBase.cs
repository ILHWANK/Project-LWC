using System;

namespace WHDle.Database.Vo
{
    using Util.Define;

    [Serializable]
    public class VOItemBase : VOBase
    {
        public ItemType ItemType;
        public int ItemCode;
        public string EnglishName;
        public string KoreanName;
        public string ItemExplain;

        public int ItemAmount
        {
            get { return ItemAmount; }
            set { ItemAmount = value; }
        }

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
