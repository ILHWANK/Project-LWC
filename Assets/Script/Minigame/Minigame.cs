using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    [SerializeField] private RectTransform outterHalfCircle;
    [SerializeField] private RectTransform innerHalfCircle;

    [SerializeField] private Image correctRangeImage;
    [SerializeField] private Image incorrectRangeImage;

    [SerializeField] private GameObject movedBar;

    [SerializeField] private Button stopButton, closeButton;

    private float outterRadius = 0f;
    private float averageRadius = 0f;

    private float halfRaduis => outterRadius / 2f;
    private Vector2 center;

    private Vector2 movedBarPosition = Vector2.zero;

    public enum Level { easy = 20, normal = 15, hard = 10 }
    private Level level = Level.hard;

    private int minCorrectRange = 0;
    private int maxCorrectRange = 1;

    private bool isMove = false;

    [SerializeField]
    private float speed = 1f;
    private float moveProgress = 0f;

    private bool negative = false;

    private void Start() { CalculatorMovedBarPosition(); }

    private void Update()
    {
        if (!isMove) return;

        MoveBar();
    }

    private void MoveBar()
    {
        var delta = Time.deltaTime * speed;

        if (moveProgress >= 100) negative = true;
        else if (moveProgress <= 0) negative = false;

        if (negative) moveProgress -= delta;
        else moveProgress += delta;

        var deg = interpolate();
        var rad = Mathf.Deg2Rad * deg;

        var x = Mathf.Cos(rad) * averageRadius;
        var y = Mathf.Sin(rad) * averageRadius - halfRaduis;

        movedBar.transform.localPosition = new Vector2(x, y);
        movedBar.transform.rotation = Quaternion.Euler(0, 0, deg);
    }

    private float interpolate() => (-180 * moveProgress / 100f);

    #region Test Code

    [SerializeField] private List<Button> buttons = new List<Button>();

    //TEST CODE
    public void SetLevel(int level)
    {
        if (level == 0)
            this.level = Level.hard;
        else if (level == 1)
            this.level = Level.normal;
        else
            this.level = Level.easy;

        buttons.ForEach(button => button.interactable = false);

        GameStart();
    }

    #endregion

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void OnClickClose()
    {
        gameObject.SetActive(false);
    }

    public void SetLevel(Level level) { this.level = level; }
    public void GameStart() 
    { 
        SetCorrectRange();

        isMove = true;

        stopButton.interactable = true;
    }

    private void CalculatorMovedBarPosition()
    {
        outterRadius = outterHalfCircle.rect.width / 2f;
        var innerCircleRadius = innerHalfCircle.rect.width / 2f;

        averageRadius = -(outterRadius + innerCircleRadius) /2;
        var posY = -halfRaduis;

        movedBarPosition = new Vector2(averageRadius, posY);

        movedBar.transform.localPosition = movedBarPosition;

        center = new Vector2(0, -halfRaduis);
    }

    private void SetCorrectRange()
    {
        int minValue = 100 - (int)level;

        minCorrectRange = Random.Range(0, minValue);
        maxCorrectRange = minCorrectRange + (int)level;

        incorrectRangeImage.fillAmount = minCorrectRange / 100f;
        correctRangeImage.fillAmount = maxCorrectRange / 100f;
    }

    public void OnClickPush()
    {
        //TEST
        buttons.ForEach(button => button.interactable = true);
        //TEST

        isMove = false;
        stopButton.interactable = false;

        if ((moveProgress >= minCorrectRange) && (moveProgress <= maxCorrectRange))
        {
            Debug.Log($"Success");

            SoundManager.instance.SFXPlay(SoundManager.SFXType.Success);
        }
        else
        {
            Debug.Log($"Faild");

            SoundManager.instance.SFXPlay(SoundManager.SFXType.Fail);
        }
    }
}
