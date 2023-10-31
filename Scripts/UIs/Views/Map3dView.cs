using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TMPro;

public class Map3dView : MonoBehaviour
{
    //視点変更ボタン
    [SerializeField] Button viewChangeButton;
    //左上のユーザーアイコン
    [SerializeField] private Button userInfoButton;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider expSlider;
    //メニュー画面内
    [SerializeField] private Button partyButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button collectionButton;
    [SerializeField] private Button achieveButton;
    [SerializeField] private Button battleButton;
    [SerializeField] private Button gachaButton;
    //ボタン押された時の処理
    public IObservable<Unit> OnSetting => settingButton.OnClickAsObservable();
    public IObservable<Unit> OnCollection => collectionButton.OnClickAsObservable();
    public IObservable<Unit> OnAchieve => achieveButton.OnClickAsObservable();
    public IObservable<Unit> OnBattle => battleButton.OnClickAsObservable();
    public IObservable<Unit> OnGacha=> gachaButton.OnClickAsObservable();
    public IObservable<Unit> OnUserInfo=> userInfoButton.OnClickAsObservable();
    public IObservable<Unit> OnParty=> partyButton.OnClickAsObservable();
    //視点変更
    public IObservable<Unit> OnViewChange => viewChangeButton.OnClickAsObservable();

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetExp(int exp,int maxExp)
    {
        expSlider.value = (float)exp / (float)maxExp;
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
