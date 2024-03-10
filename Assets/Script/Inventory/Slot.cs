using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WHDle.Database.Vo;
using WHDle.Util;

namespace WHDle.UI.Inventory
{
    public class Slot : MonoBehaviour, IPoolableObject
    {
        public bool CanRecyle { get; set; } = true;
        public Action OnRecyleStart { get; set; }

        private VOItemBase voItemBase;
        public VOItemBase VoItemBase { get { return voItemBase; } }

        public GameObject SlotInner;
        public Image ItemImage;
        public TMP_Text AmountText;

        public void DeleteItem()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;

            SlotInner.SetActive(false);
        }

        public void SetItem(VOItemBase voItemBase)
        {
            this.voItemBase = voItemBase;


            //ItemImage.sprite = Resources.Load<Sprite>(voItemBase.EnglishName);

        }

        public void SlotClear()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;
        }
    }
}
