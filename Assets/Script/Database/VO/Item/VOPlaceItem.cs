using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.Vo
{
    using SD;
    using WHDle.Util;

    [Serializable]
    public class VOPlaceItem : VOItemBase
    {
        public SDPlaceItem sdPlaceItem;

        public VOPlaceItem() { }
        public VOPlaceItem(string itemCode, int amount) 
        {
            sdPlaceItem = FindPlaceItem(itemCode);

            ItemType = GetItemType(sdPlaceItem.PlaceItemNumber, 2);
            ItemCode = GetItemCode(sdPlaceItem.PlaceItemNumber, 3);
            EnglishName = sdPlaceItem.PlaceItemNameEN;
            KoreanName = sdPlaceItem.PlaceItemNameKR;
            ItemExplain = sdPlaceItem.PlaceItemExplain;

            ItemAmount = amount;
        }

        private SDPlaceItem FindPlaceItem(string itemCode)
        {
            return GameManager.SD.sdPlaceItems.Where(spi => spi.PlaceItemNumber == itemCode).SingleOrDefault();
        }
    }
}
