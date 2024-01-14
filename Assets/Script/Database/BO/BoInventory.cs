using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.BO
{
    using Dto;
    using LitJson;
    using WHDle.Util;

    [Serializable]
    public class BoInventory : BoBase
    {
        public List<BoItem> Items = new();

        public BoInventory(DtoInventory dtoInventory)
        {
            if (dtoInventory == null) return;

            if (dtoInventory.ItemIndexes == string.Empty || dtoInventory.ItemAmounts == string.Empty) return;

            var itemIndexes = JsonMapper.ToObject<List<int>>(new JsonReader(dtoInventory.ItemIndexes));
            var itemAmounts = JsonMapper.ToObject<List<int>>(new JsonReader(dtoInventory.ItemAmounts));

            var items = GameManager.SD.sdItems;

            for(int i = 0; i < itemIndexes.Count; i++)
            {
                var itemIndex = itemIndexes[i];
                var itemAmount = itemAmounts[i];

                var sdItem = items.Where(item => item.ItemIndex == itemIndex).SingleOrDefault();

                var boItem = new BoItem();

                boItem.SDItem = sdItem;
                boItem.Amount = itemAmount;

                Items.Add(boItem);
            }
        }
    }
}
