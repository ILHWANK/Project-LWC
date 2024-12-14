using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Database.Vo;

namespace WHDle.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private Transform gridTransfrom;

        [SerializeField]
        private List<InventoryItemSlot> slots = new List<InventoryItemSlot>();

        private bool isFirstEnable = true;

        public void Start()
        {
            isFirstEnable = false;
        }

        public void OnEnable()
        {
            if (isFirstEnable) return;

            UpdateAllSlots();
        }

        public void UpdateAllSlots()
        {
            for(int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];

                // if (slot.VoItemBase != null)
                //     slot.SetItem(slot.VoItemBase);
                // else
                //     slot.SlotClear();
            }
        }

        public void SetSlot(int slotNumber, VOItemBase itemBase)
        {
            // slots[slotNumber].SetItem(itemBase);
        }

        // public void ClearSlot(int slotNumber)
        //     => slots[slotNumber].SlotClear();
    }
}