using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using WHDle.Util;

namespace WHDle.UI.Inventory
{
    using Util.Define;
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private GameObject inventoryObject;

        [SerializeField]
        private Transform gridTransform;

        public bool isUpdate = true;

        private int currentSlotCount = 0;

        private List<Slot> slots = new List<Slot>();

        public void OpenInventory()
        {
            inventoryObject.SetActive(true);

            if (!isUpdate) return;

            UpdateSlot();

            isUpdate = false;
        }

        public void CloseInventory()
        {
            inventoryObject.SetActive(false);

            GameManager.User.VoInventory.TempAdd();
            isUpdate = true;
        }

        private void UpdateSlot()
        {
            var voInventory = GameManager.User.VoInventory;

            var slotCount = voInventory.ItemSlotCount;

            if (slotCount == 0) return;

            if (slotCount > currentSlotCount)
                AddSlot(slotCount);
            else if (slotCount < currentSlotCount)
                DeleteSlot(slotCount);

            currentSlotCount = slotCount;

            SetItems();
        }

        private void AddSlot(int slotCount)
        {
            var pools = ObjectPoolManager.Instance.GetPool<Slot>(PoolType.Slot);

            var addSlot = slotCount - currentSlotCount;

            for (int i = 0; i < addSlot; i++)
                slots.Add(pools.GetPoolableObject(pool => pool.CanRecyle));

            foreach(var slot in slots)
            {
                slot.gameObject.SetActive(true);
                slot.transform.SetParent(gridTransform);
            }
        }

        private void DeleteSlot(int slotCount)
        {
            var pools = ObjectPoolManager.Instance.GetPool<Slot>(PoolType.Slot);

            var deleteSlot = currentSlotCount - slotCount;

            var slot = slots.GetRange(slots.Count - (deleteSlot + 1), deleteSlot);

            foreach(var s in slot)
            {
                s.gameObject.SetActive(false);
                s.transform.parent = pools.holder;
                slots.Remove(s);
            }
                
        }

        private void SetItems()
        {
            var voInventory = GameManager.User.VoInventory;

            var voMake = voInventory.voMake;
            var voPlaceItems = voInventory.voPlaceItem;

            var vmCount = voMake.Count;

            for(int i = 0; i < vmCount; i++)
            {
                var vm = voMake[i];

                slots[i].SetSprite(vm);
                slots[i].SetAmount(vm.ItemAmount);
            }

            for(int i = vmCount; i < vmCount + voPlaceItems.Count; i++)
            {
                var vp = voPlaceItems[i - vmCount];

                slots[i].SetSprite(vp);
                slots[i].SetAmount(vp.ItemAmount);
            }

        }
    }
}