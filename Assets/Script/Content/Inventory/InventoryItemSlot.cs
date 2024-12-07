using System;
using script.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WHDle.Database.Vo;
using WHDle.Util;

namespace WHDle.UI.Inventory
{
    public class InventoryItemSlot : MonoBehaviour, IPoolableObject
    {
        public struct Data
        {
            public string ItemId;
        }

        private Data _data;
        
        [SerializeField] private Button _click;
        [SerializeField] private UIPopup _inventoryResult;
        
        public bool CanRecyle { get; set; } = true;
        public Action OnRecyleStart { get; set; }

        private VOItemBase voItemBase;
        public VOItemBase VoItemBase { get { return voItemBase; } }

        public GameObject SlotInner;
        public Image ItemImage;
        public TMP_Text AmountText;

        public void Start()
        {
            _click.onClick.AddListener(OnClicked);
        }

        public void SetData(Data data)
        {
            _data = data;
            
            Init();
        }
        
        public void Reset()
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
            //ItemImage.sprite = null;
            //AmountText.text = string.Empty;
        }

        private void Init()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;

            SlotInner.SetActive(true);
        }

        private void Refresh()
        {
            Debug.Log(_data.ItemId);
        }

        #region Event

        private void OnClicked()
        {
           UIManager.Instance.OpenPopup("InventoryItemInfoPopup"); 
        }

        #endregion=
    }
}
