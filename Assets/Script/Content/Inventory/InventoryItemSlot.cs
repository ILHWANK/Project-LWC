using System;
using script.Common;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
            
            Init();
        }

        public void SetData(Data data)
        {
            _data = data;
            
            Refresh();
        }

        private void Init()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;

            SlotInner.SetActive(false);
        }

        private void Refresh()
        {
            AmountText.text = string.Empty;
            
            var itemTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/ItemTable.csv");

            var itemData = itemTable.GetByColumn("Item_Id", _data.ItemId);
            
            var spriteAddress = itemData["Item_Path"];
            
            Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    ItemImage.sprite = handle.Result;
                    
                    SlotInner.SetActive(true);
                }
                else
                {
                    SlotInner.SetActive(false);
                }
            };
        }

        #region Event

        private void OnClicked()
        {
           var inventoryItemInfoPopup = UIManager.Instance.GetPopup("InventoryItemInfoPopup");

           inventoryItemInfoPopup.GetComponent<InventoryItemInfoPopup>();

           var itemData = new InventoryItemInfoPopup.ItemData
           {
               ItemId = _data.ItemId
           };
           
           UIManager.Instance.OpenPopup<InventoryItemInfoPopup>("InventoryItemInfoPopup", 
               popup =>
               {
                   popup.SetData(itemData);
               }); 
        }

        #endregion=
    }
}
