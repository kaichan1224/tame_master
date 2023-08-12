using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

/// <summary>
/// プレイヤーに関するユーザデータを管理する
/// </summary>
public class PlayerInfoPageManager : MonoBehaviour
{
    //各UI要素
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text nameText;
    //閉じるボタン
    [SerializeField] private Button backButton;
    //3dマップが表示されるページ
    [SerializeField] private GameObject map3dPage;
    //レベルのテキスト
    [SerializeField] private TMP_Text levelText;
    //Expのテキスト
    [SerializeField] private TMP_Text expText;
    //ExpのSlider
    [SerializeField] private Slider expSlider;
    //総Kcalのテキスト
    [SerializeField] private TMP_Text totalKcalText;
    //総移動距離のテキスト
    [SerializeField] private TMP_Text moveDistanceText;
    //ユーザデータ管理部
    [SerializeField] private UserDataManager userDataManager;
    //左上のアイコンの画像
    [SerializeField] private Image Charactor;
    //ガリガリの画像
    [SerializeField] private Sprite Garigari;
    //普通の時の画像
    [SerializeField] private Sprite Normal;
    //ムキムキの画像
    [SerializeField] private Sprite Mukimuki;
    /// <summary>
    /// 画面が表示された時の処理
    /// </summary>
    private void Start()
    {
        inputField.text = userDataManager.playerName;
        //ゲーム開始の表示アイコンの設定
        if (userDataManager.level.Value >= 1 && userDataManager.level.Value <= 9)
        {
            Charactor.sprite = Garigari;
        }
        else if (userDataManager.level.Value >= 10 && userDataManager.level.Value <= 19)
        {
            Charactor.sprite = Normal;
        }
        else
        {
            Charactor.sprite = Mukimuki;
        }
        //閉じるボタンを押された時の処理
        backButton.onClick.AddListener(() =>
        {
            map3dPage.SetActive(true);
            this.gameObject.SetActive(false);
        });
        //名前の入力処理
        inputField.onEndEdit.AddListener(UpdateText);
        //ユーザデータの値が変更された時にUIに反映されるようにする
        userDataManager.level
            .Where(value => value == 10)
            .Subscribe(_ => Charactor.sprite = Normal)
            .AddTo(this);

        userDataManager.level
            .Where(value => value == 20)
            .Subscribe(_ => Charactor.sprite = Mukimuki)
            .AddTo(this);
        userDataManager.level
            .Subscribe(x => levelText.text = x.ToString())
            .AddTo(this);
        userDataManager.totalKcal
            .Subscribe(x => totalKcalText.text = x.ToString("N1") + "kcal")
            .AddTo(this);
        userDataManager.distanceTraveled
            .Subscribe(x => moveDistanceText.text = x.ToString("N2") + "km")
            .AddTo(this);
        userDataManager.exp
            .Subscribe(x =>
            {
                expText.text = x.ToString() + "/" + userDataManager.CalculateRequiredExp().ToString();
                expSlider.value = (float)x/ (float)userDataManager.CalculateRequiredExp();
            })  
            .AddTo(this);
    }

    private void UpdateText(string text)
    {
        nameText.text = text;
        userDataManager.playerName = text;
    }
}

