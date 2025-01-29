using System;
using System.Collections.Generic;
using script.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Content.Dialogue
{
    public class UIDialoguePopup : UIPopup
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI contextText;

        [SerializeField] private List<Button> choiceButtons;

        [SerializeField] private RawImage characterImageSpot1;
        [SerializeField] private RawImage characterImageSpot2;

        [SerializeField] private Button screenButton;
        [SerializeField] private Button skipButton;

        private void Start()
        {
            AddListener();

            HideAllUI();
        }

        private void AddListener()
        {
            // screenButton.onClick.AddListener(OnScreenButtonClicked);
            skipButton.onClick.AddListener(OnSkipButtonClicked);
        }

        public void HideAllUI()
        {
            foreach (var button in choiceButtons)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }

            characterImageSpot1.gameObject.SetActive(false);
            characterImageSpot2.gameObject.SetActive(false);
        }

        public void SetDialogue(string characterName, string text)
        {
            nameText.text = characterName;
            contextText.text = text;
        }

        public void SetCharacter(string characterName, string spritePath)
        {
            var characterSprite = Resources.Load<Sprite>(spritePath);

            if (characterSprite == null) return;

            if (characterName == "마녀")
            {
                characterImageSpot1.texture = characterSprite.texture;
                characterImageSpot1.gameObject.SetActive(true);
            }
            else
            {
                characterImageSpot2.texture = characterSprite.texture;
                characterImageSpot2.gameObject.SetActive(true);
            }
        }

        public void ShowChoices(List<string> choices, Action<int> onChoiceSelected)
        {
            for (var i = 0; i < choices.Count; i++)
            {
                if (i >= choiceButtons.Count) break;

                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];

                var choiceIndex = i;
                choiceButtons[i].onClick.AddListener(() => onChoiceSelected(choiceIndex));
            }
        }

        private void OnSkipButtonClicked()
        {
            UIManager.Instance.ClosePopup("DialoguePopup");
        }

        private void OnScreenButtonClicked()
        {
            Debug.Log("다음 Context 출력");
        }
    }
}