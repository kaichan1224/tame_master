using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using System;

public class InGameTameView : MonoBehaviour
{
    [SerializeField] private Slider natukiGage;
    [SerializeField] private RectTransform fillArea;
    [SerializeField] private RectTransform targetArea;
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private Button tapButton;
    [SerializeField] private Button tameButton;
    [SerializeField] private Button esaButton;
    [SerializeField] private TMP_Text esaText;
    [SerializeField] private TMP_Text esaButtonText;

    public IObservable<Unit> OnTap => tapButton.OnClickAsObservable();
    public IObservable<Unit> OnTame => tameButton.OnClickAsObservable();
    public IObservable<Unit> OnEsa => esaButton.OnClickAsObservable();

    /// <summary>
    /// 現在のなつき度
    /// </summary>
    /// <param name="currentValue"></param>
    public void SetCurrentNatukido(int currentValue)
    {
        natukiGage.value = (float)currentValue/(float)100;
    }

    public float GetWidth()
    {
        return fillArea.sizeDelta.x;
    }

    public void SetEsaButtonText(int Num)
    {
        esaButtonText.text = $"所持数　{Num}";
    }

    /// <summary>
    /// 成功ゲージの場所を指定する
    /// from% ~ to%の範囲で成功(0~100)
    /// </summary>
    public void SetTargetArea(int from,int to)
    {
        //成功ゲージのサイズを指定
        float targetWidth = ((float)(to - from) / 100) * GetWidth();
        var size = targetArea.sizeDelta;
        size.x = targetWidth;
        targetArea.sizeDelta = size;
        float targetPosX = ((float)from/100) * GetWidth() + targetWidth / 2;
        float targetPosY = targetArea.anchoredPosition.y;
        targetArea.anchoredPosition = new Vector2(targetPosX,targetPosY);
    }

    public void SetCountDownText(string text)
    {
        countDownText.text = text;
    }

    public void SetEsaText(string text)
    {
        esaText.text = text;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetActiveTame(bool flag)
    {
        natukiGage.gameObject.SetActive(flag);
        tapButton.gameObject.SetActive(flag);
    }

    public void InActiveEsa()
    {
        esaButton.interactable = false;
    }

    public void SetActiveSelect(bool flag)
    {
        tameButton.gameObject.SetActive(flag);
        esaButton.gameObject.SetActive(flag);
    }
}
