using JetBrains.Annotations;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WHDle.Util;

namespace WHDle.Database.Dto
{
    [SerializeField]
    public class DtoInventory : DtoBase
    {
        public string ItemIndexes;
        public string ItemAmounts;

        public DtoInventory()
        {
            var boInventory = GameManager.User.boInventory;

            if (boInventory == null)
            {
                this.ItemIndexes = string.Empty;
                this.ItemAmounts = string.Empty;
            }

            int[] ItemIndexes = boInventory.Items.Select(item => item.SDItem.ItemIndex).ToArray();
            int[] ItemAmounts = boInventory.Items.Select(item => item.Amount).ToArray();

            this.ItemIndexes = JsonMapper.ToJson(ItemIndexes);
            this.ItemAmounts = JsonMapper.ToJson(ItemAmounts);
        }
    }
}