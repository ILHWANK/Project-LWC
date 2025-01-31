using System;
using System.Collections;
using System.Collections.Generic;
using script.Common;
using TMPro;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Script.Content.Dialogue
{
    public class UIDialoguePopup : UIPopup
    {
        [SerializeField] private List<UIDialogueCharacterSlot> uiDialogueCharacterSlotList;
        
        [SerializeField] private Button screenButton;
        [SerializeField] private Button skipButton;

        private void Start()
        {
            AddListener();

            HideAllCharacters();
        }

        private void AddListener()
        {
            skipButton.onClick.AddListener(OnSkipButtonClicked);
        }
        
        public void HideAllCharacters()
        {
            foreach (var slot in uiDialogueCharacterSlotList)
            {
                slot.gameObject.SetActive(false);
            }
        }

        public void ChangeCharacters(string[] characterNames, Action onComplete)
        {
            StartCoroutine(LoadCharactersCoroutine(characterNames, onComplete));
        }

        private IEnumerator LoadCharactersCoroutine(string[] characterNames, Action onComplete)
        {
            var handles = new List<AsyncOperationHandle<Texture2D>>();

            for (int i = 0; i < uiDialogueCharacterSlotList.Count; i++)
            {
                if (i < characterNames.Length && !string.IsNullOrEmpty(characterNames[i]))
                {
                    uiDialogueCharacterSlotList[i].gameObject.SetActive(true);
                    handles.Add(uiDialogueCharacterSlotList[i].ChangeImage(characterNames[i]));
                }
                else
                {
                    uiDialogueCharacterSlotList[i].gameObject.SetActive(false);
                }
            }

            foreach (var handle in handles)
            {
                yield return handle; // ✅ 비동기 로딩이 끝날 때까지 대기
            }

            onComplete?.Invoke(); // ✅ 로딩 완료 후 콜백 실행
        }
        
        #region Event
        
        private void OnSkipButtonClicked()
        {
            
        }

        #endregion
    }
}