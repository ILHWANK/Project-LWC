using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.SD
{
    [Serializable]
    public class SDPlaceItem : StaticBase
    {
        public string PlaceItemNumber;
        public string PlaceItemNameEN;
        public string PlaceItemNameKR;
        public string PlaceItemExplain;
        public int PlaceItemDropCount;

        public char GetPlaceItemCode() => PlaceItemNumber.First();
        public int GetPlaceItemNumber()
        {
            var s = PlaceItemNumber[^0..];

            return int.Parse(s);
        }
    }
}
