using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using System;

public class tameStartView : MonoBehaviour
{
    [SerializeField] private Image player;
    [SerializeField] private Image animal;
    [SerializeField] private RectTransform vsPlayerPanel;
    [SerializeField] private RectTransform vsAnimalPanel;
    [SerializeField] private Image flashPanelImage;
    [SerializeField] private RectTransform flashPanel;
    [SerializeField] private RectTransform vsImageRect;
    [SerializeField] private Image vsImage;
    public void StartTame(Sprite player,Sprite animal,Action endFunc)
    {
        //this.player.sprite = player;
        this.animal.sprite = animal;
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendInterval(0.1f)
            .Append(flashPanelImage.DOFade(endValue: 1f, duration: 0.15f))
            .Append(flashPanelImage.DOFade(endValue: 0f, duration: 0.15f))
            .Append(flashPanelImage.DOFade(endValue: 1f, duration: 0.15f))
            .Append(flashPanelImage.DOFade(endValue: 0f, duration: 0.15f))
            .Append(vsPlayerPanel.DOAnchorPos(new Vector2(-988, 0), 0.5f).SetEase(Ease.InOutQuart))
            .Join(vsAnimalPanel.DOAnchorPos(new Vector2(-835, 0), 0.5f).SetEase(Ease.InOutQuart))
            .AppendInterval(0.1f)
            .Append(vsImage.DOFade(endValue: 1f, duration: 0.3f))
            .Append(vsImageRect.DOScale(4f, 1f))
            .OnComplete(()=>
            {
                endFunc?.Invoke();

            }); // UIアニメーション完了後に呼び出すメソッドを指定
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
