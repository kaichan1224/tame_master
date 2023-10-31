using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using UnityEngine.UI;
using System;
using TMPro;


public class GachaView : MonoBehaviour
{
    //閉じるボタン
    [SerializeField] private Button backButton;
    [SerializeField] private Button gachaButton;
    [SerializeField] private TMP_Text gachaTicketNumText;
    [SerializeField] private GachaResultPanel gachaResultPanel;
    [SerializeField] private GameObject brockObject;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public IObservable<Unit> OnGacha => gachaButton.OnClickAsObservable();
    public IObservable<Unit> OnBackResult => gachaResultPanel.GetBackButton().OnClickAsObservable();

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetActiveGachaButton(bool flag)
    {
        gachaButton.interactable = flag;
    }


    public void SetGachaTicketNumText(int cnt)
    {
        gachaTicketNumText.text = $"ガチャチケット所持数:{cnt}枚";
    }

    public void SetResultPanel(ItemParam itemParam,Sprite itemSprite)
    {
        gachaResultPanel.Init(itemParam,itemSprite);
    }

    public void ShowResultPanel()
    {
        gachaResultPanel.Show();
    }

    public void HideResultPanel()
    {
        gachaResultPanel.Hide();
    }

    public void SetActiveBrock(bool flag)
    {
        brockObject.SetActive(flag);
    }
}
