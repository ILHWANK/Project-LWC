using LitJson;
using System;
using System.Collections.Generic;

namespace WHDle.Database.Vo
{
    using Dto;
    using System.Linq;
    using Util.Define;

    [Serializable]
    public class VOInventory : VOBase
    {
        public List<VOPlaceItem> voPlaceItem = new();
        public List<VOMake> voMake = new();

        public int SlotCounter = 0;

        private bool isUpdated = false;
        public bool IsUpdated { get { return isUpdated; } }

        public VOInventory(DtoInventory dtoInventory)
        {
            var dPIIs = JsonMapper.ToObject<List<string>>(new JsonReader(dtoInventory.PlaceItemIndexes));
            var dPIAs = JsonMapper.ToObject<List<int>>(new JsonReader(dtoInventory.PlaceItemAmounts));

            var dMIs = JsonMapper.ToObject<List<string>>(new JsonReader(dtoInventory.MakeItemIndexes));
            var dMAs = JsonMapper.ToObject<List<int>>(new JsonReader(dtoInventory.MakeItemAmounts));

            for (int i = 0; i < dPIIs.Count; i++)
            {
                var item = dPIIs[i];
                var amount = dPIAs[i];

                VOPlaceItem vpi = new(item, amount);
                voPlaceItem.Add(vpi);
            }

            for (int i = 0; i < dMIs.Count; i++)
            {
                var item = dMIs[i];
                var amount = dMAs[i];

                VOMake vm = new(item, amount);
                voMake.Add(vm);
            }

            SlotCounter = dtoInventory.SlotCounter;
        }


    }
}
