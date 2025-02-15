using System.Collections;
using Script.Common.UI;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    [SerializeField] private DialogueRunner dialogueRunner;
    // [SerializeField] private UIDialoguePopup dialoguePopup;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        dialogueRunner.AddCommandHandler("end", EndDialogue);

        // ✅ MethodInfo 방식으로 changeCharacter 등록
        dialogueRunner.AddCommandHandler<string[]>("changeCharacter", ChangeCharacter);
    }

    public void StartDialogue(string dialogueNode)
    {
        UIManager.Instance.SetDialogue(true);

        dialogueRunner.StartDialogue(dialogueNode);
    }

    #region Handler

    // ✅ MethodInfo를 사용해야 하므로 private → public 변경 필요
    private IEnumerator ChangeCharacter(params string[] characterNames)
    {
        bool isLoaded = false;

        // ✅ 매개변수가 없으면 모든 캐릭터 비활성화
        if (characterNames == null || characterNames.Length == 0)
        {
            MessageSystem.Instance.Publish("HideCharacters", "CharacterName");
            yield break; // 모든 캐릭터 숨기고 종료
        }

        // ✅ 여러 캐릭터 변경 요청 (콜백을 활용)
        // dialoguePopup.ChangeCharacters(characterNames, () => { isLoaded = true; });

        // ✅ 모든 캐릭터 로딩이 완료될 때까지 대기
        yield return new WaitUntil(() => isLoaded);
    }

    private void EndDialogue()
    {
        UIManager.Instance.SetDialogue(false);
    }

    #endregion
}