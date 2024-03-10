using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Controller
{
    using Util.Define;
    using UI.Inventory;
    using WHDle.Database.Vo;
    using WHDle.Util;
    using System.Linq;

    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private GameObject inventoryObject;

        [SerializeField]
        private InventoryUI inventoryUI;

        private VOInventory voInventory;

        [SerializeField]
        public ItemType inventoryType = ItemType.PI;

        private readonly int MAX_SLOT_COUNT = 25;

        private int placeItemSlot = 0;
        private int makeItemSlot = 0;

        private int placeItemRemainSlot => MAX_SLOT_COUNT - placeItemSlot;
        private int makeItemRemainSlot => MAX_SLOT_COUNT - makeItemSlot;

        private bool existEmptyPlaceItemSlot => placeItemRemainSlot > 0;
        private bool existEmptyMakeItemSlot => makeItemRemainSlot > 0;

        private ItemType currentInventoryType = ItemType.PI;

        public void OpenInventory()
        {
            inventoryObject.SetActive(true);

            SetInventory();
        }

        public void CloseInventory()
        {
            inventoryObject.SetActive(false);
        }

        public void Start()
        {
            CloseInventory();

            RefreshInventoryDatabase();
        }

        public void OnEnable()
        {
            RefreshInventoryDatabase();
        }

        private void RefreshInventoryDatabase() => voInventory = GameManager.User?.VoInventory;

        private void SetInventory()
        {
            if (currentInventoryType == ItemType.PI)
                SetInventory_PlaceItem();
            else if (currentInventoryType == ItemType.M)
                SetInventory_MakeItem();
        }

        private void SetInventory_PlaceItem()
        {
            var pis = voInventory.voPlaceItem;
            
            for(int i = 0; i< MAX_SLOT_COUNT; i++)
            {
                if (i < pis.Count)
                    inventoryUI.SetSlot(i, pis[i]);
                else
                    inventoryUI.ClearSlot(i);
            }
        }

        private void SetInventory_MakeItem()
        {
            var ms = voInventory.voMake;

            for(int i = 0; i < MAX_SLOT_COUNT; i++)
            {
                if (i < ms.Count)
                    inventoryUI.SetSlot(i, ms[i]);
                else
                    inventoryUI.ClearSlot(i);
            }
        }

        public void AddItem(ItemType itemType,string ItemCode, int amount)
        {
            if (itemType == ItemType.P) return;

            if (itemType == ItemType.PI)
                AddPlaceItem(ItemCode, amount);
            else if (itemType == ItemType.M)
                AddMakeItem(ItemCode, amount);

            if (itemType == currentInventoryType)
            {
                SetInventory();
            }
        }

        private void AddPlaceItem(string itemCode, int amount)
        {
            if (!existEmptyPlaceItemSlot) return;

            var item = voInventory.voPlaceItem.Where(vpi => vpi.sdPlaceItem.PlaceItemNumber == itemCode).SingleOrDefault();

            if (item == null)
            {
                voInventory.voPlaceItem.Add(new VOPlaceItem(itemCode, amount));
                
                ++placeItemSlot;
            }
            else
                item.ItemAmount += amount;


        }

        private void AddMakeItem(string itemCode, int amount)
        {
            if (!existEmptyMakeItemSlot) return;

            var item = voInventory.voMake.Where(vm => vm.sdMake.MakeItemNumber == itemCode).SingleOrDefault();

            if (item == null)
            {
                voInventory.voMake.Add(new VOMake(itemCode, amount));

                ++makeItemSlot;
            }
            else
                item.ItemAmount += amount;
        }

        public void DeleteItem(ItemType itemType, string itemCode, int amount)
        {
            if (itemType == ItemType.P) return;

            if (itemType == ItemType.PI)
                DeleteItem_PlaceItem(itemCode, amount);
            else if (itemType == ItemType.M)
                DeleteItem_MakeItem(itemCode, amount);     

        }

        private void DeleteItem_PlaceItem(string itemCode, int amount)
        {
            var item = voInventory.voPlaceItem.Where(vpi => vpi.sdPlaceItem.PlaceItemNumber == itemCode).SingleOrDefault();

            if (item == null || item.ItemAmount < amount) return;

            if (item.ItemAmount == amount)
            {
                voInventory.voPlaceItem.Remove(item);
                placeItemSlot--;
            }
            else
                item.ItemAmount -= amount;
        }

        private void DeleteItem_MakeItem(string itemCode, int amount)
        {
            var item = voInventory.voMake.Where(vm => vm.sdMake.MakeItemNumber == itemCode).SingleOrDefault();

            if (item == null || item.ItemAmount < amount) return;

            if (item.ItemAmount == amount)
            {
                voInventory.voMake.Remove(item);
                makeItemSlot--;
            }
            else
                item.ItemAmount -= amount;
        }
    }
}
