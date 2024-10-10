using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvDialogueParser
{
    public List<DialogueEvent> ParseDialogueEvents(string filePath)
    {
        List<DialogueEvent> dialogueEvents = new List<DialogueEvent>();
        string[] lines = File.ReadAllLines(filePath); // CSV 파일의 모든 줄을 읽음
        string[] headers = lines[0].Split(','); // 첫 번째 줄에서 헤더를 추출

        for (int i = 1; i < lines.Length; i++) // 각 줄을 순회
        {
            string[] fields = lines[i].Split(','); // 각 줄의 필드를 ','로 구분하여 배열로 만듦
            DialogueEvent dialogueEvent = new DialogueEvent();

            // 헤더에 맞춰 각 필드를 매핑
            dialogueEvent.characterName = fields[Array.IndexOf(headers, "characterName")];
            dialogueEvent.choiceGroup = fields[Array.IndexOf(headers, "choiceGroup")];

            // 라인 범위 처리
            dialogueEvent.line = new Vector2(
                float.Parse(fields[Array.IndexOf(headers, "lineStart")]), 
                float.Parse(fields[Array.IndexOf(headers, "lineEnd")])
            );

            // Dialogue 배열 생성
            Dialogue dialogue = new Dialogue();
            dialogue.cameraType = (DialogueEnum.CameraActionType)Enum.Parse(typeof(DialogueEnum.CameraActionType), fields[Array.IndexOf(headers, "cameraType")]);

            dialogue.contextName = fields[Array.IndexOf(headers, "contextName")];
            dialogue.skipContext = fields[Array.IndexOf(headers, "skipContext")];
            dialogue.dialogueType = (DialogueEnum.DialogueType)Enum.Parse(typeof(DialogueEnum.DialogueType), fields[Array.IndexOf(headers, "dialogueType")]);

            // 배열 데이터 분리
            dialogue.contexts = fields[Array.IndexOf(headers, "contexts")].Split('|');
            dialogue.spriteNames = fields[Array.IndexOf(headers, "spriteNames")].Split('|');

            // 카메라 액션을 배열로 변환
            string[] cameraActionStrings = fields[Array.IndexOf(headers, "cameraActions")].Split('|');
            dialogue.cameraActions = Array.ConvertAll(cameraActionStrings, action => (DialogueEnum.CameraActionType)Enum.Parse(typeof(DialogueEnum.CameraActionType), action));

            dialogueEvent.dialogues = new Dialogue[] { dialogue };

            dialogueEvents.Add(dialogueEvent);
        }

        return dialogueEvents;
    }
}
