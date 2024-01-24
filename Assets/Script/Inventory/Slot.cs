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

        public Image ItemImage;
        public TMP_Text AmountText;

        public void SetSprite(VOItemBase voItemBase)
        {
            /*ItemImage.sprite = Resources.Load<Sprite>(voItemBase.EnglishName);*/
        }

        public void SetAmount(int amount)
        {
            AmountText.text = amount.ToString();
        }
    }
}
