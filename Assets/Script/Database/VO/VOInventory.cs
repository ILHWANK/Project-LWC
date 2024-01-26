using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHDle.Database.Dto;
using WHDle.Util;

namespace WHDle.Database.Vo
{
    [Serializable]
    public class VOInventory : VOBase
    {
        public int ItemSlotCount;

        public List<VOPlaceItem> voPlaceItem = new();
        public List<VOMake> voMake = new();

        public VOInventory(DtoInventory dtoInventory)
        {
            var dPIIs= JsonMapper.ToObject<List<string>>(new JsonReader(dtoInventory.PlaceItemIndexes));
            var dPIAs = JsonMapper.ToObject<List<int>>(new JsonReader(dtoInventory.PlaceItemAmounts));

            var dMIs = JsonMapper.ToObject<List<string>>(new JsonReader(dtoInventory.MakeItemIndexes));
            var dMAs = JsonMapper.ToObject<List<int>>(new JsonReader(dtoInventory.MakeItemAmounts));

            ItemSlotCount = dPIIs.Count + dMIs.Count;

            for (int i = 0; i < dPIIs.Count; i++)
            {
                var item = dPIIs[i];
                var amount = dPIAs[i];

                VOPlaceItem vpi = new(item, amount);
                voPlaceItem.Add(vpi);
            }

            for(int i = 0; i < dMIs.Count; i++)
            {
                var item = dMIs[i];
                var amount = dMAs[i];

                VOMake vm = new(item, amount);
                voMake.Add(vm);
            }
        }
    }
}
