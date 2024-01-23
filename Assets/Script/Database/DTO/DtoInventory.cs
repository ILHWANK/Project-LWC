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
        public string MakeItemIndexes;
        public string MakeItemAmounts;

        public string PlaceItemIndexes;
        public string PlaceItemAmounts;

        public DtoInventory()
        {
            var voInventory = GameManager.User.VoInventory;

            if (voInventory == null)
            {
                this.MakeItemIndexes = string.Empty;
                this.MakeItemAmounts = string.Empty;

                this.PlaceItemIndexes = string.Empty;
                this.PlaceItemAmounts = string.Empty;

                return;
            }

            var MakeItemIndexes = voInventory.voMake.Select(vm => vm.sdMake.MakeItemNumber).ToList();
            var MakeItemAmounts = voInventory.voMake.Select(vm => vm.ItemAmount).ToList();

            var PlaceItemIndexes = voInventory.voPlaceItem.Select(vpi => vpi.sdPlaceItem.PlaceItemNumber).ToList();
            var PlaceItemAmounts = voInventory.voPlaceItem.Select(vpi => vpi.ItemAmount).ToList();

            this.MakeItemIndexes = JsonMapper.ToJson(MakeItemIndexes);
            this.MakeItemAmounts = JsonMapper.ToJson(MakeItemAmounts);

            this.PlaceItemIndexes = JsonMapper.ToJson(PlaceItemIndexes);
            this.PlaceItemAmounts = JsonMapper.ToJson(PlaceItemAmounts);
        }
    }
}