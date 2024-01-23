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
    public class VOMake : VOItemBase
    {
        public SDMake sdMake;

        public VOMake() { }
        public VOMake(string itemCode, int amount) 
        {
            sdMake = FindMakeItem(itemCode);

            ItemType = GetItemType(sdMake.MakeItemNumber);
            ItemCode = GetItemCode(sdMake.MakeItemNumber);
            EnglishName = sdMake.MakeItemNameEN;
            KoreanName = sdMake.MakeItemNameKR;
            ItemExplain = sdMake.MakeItemExplain;

            ItemAmount = amount;
        }

        private SDMake FindMakeItem(string itemCode)
        {
            return GameManager.SD.sdMakes.Where(spi => spi.MakeItemNumber == itemCode).SingleOrDefault();
        }
    }
}
