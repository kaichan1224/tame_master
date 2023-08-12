using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

/// <summary>
/// 3dマップページにおけるUIの処理を行うモジュール
/// </summary>
public class Map3dPageManager : MonoBehaviour
{
    //ユーザデータ管理部
    [SerializeField] private UserDataManager userDataManager;
    //3dと2dを切り替えるボタン
    [SerializeField] private Button mapSwitchButton;
    //プロフィールに飛ぶボタン
    [SerializeField] private Button playerInfoPageButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button collectionButton;
    [SerializeField] private Button achieveButton;
    //ExpのSlider
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text levelText;
    //2dマップのページ
    [SerializeField] private GameObject map2dPage;
    //2dマップを表示するカメラ
    [SerializeField] private GameObject camera2d;
    //3dマップを表示するカメラ
    [SerializeField] private GameObject camera3d;
    //2dマップ
    [SerializeField] private GameObject map2d;
    //3dマップ
    [SerializeField] private GameObject map3d;
    //ユーザー情報ページ
    [SerializeField] private GameObject playerInfoPage;
    [SerializeField] private GameObject settingPage;
    [SerializeField] private GameObject collectionPage;
    [SerializeField] private GameObject achievePage;
    [SerializeField] private GameObject moveButton;
    //左上のアイコンの画像
    [SerializeField] private Image Charactor;
    //ガリガリの画像
    [SerializeField] private Sprite Garigari;
    //普通の時の画像
    [SerializeField] private Sprite Normal;
    //ムキムキの画像
    [SerializeField] private Sprite Mukimuki;
    /// <summary>
    /// 3dマップページを起動した時に処理を行うメソッド
    /// </summary>
    private void Start()
    {
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


        //マップスイッチボタンを押された時の処理を行う
        mapSwitchButton.onClick.AddListener(() =>
        {
            map2dPage.SetActive(true);
            camera3d.SetActive(false);
            camera2d.SetActive(true);
            map3d.SetActive(false);
            map2d.SetActive(true);
            this.gameObject.SetActive(false);
        });
       
        //ユーザ情報ページボタンを押した時の処理を行う
        playerInfoPageButton.onClick.AddListener(() =>
        {
            playerInfoPage.SetActive(true);
            this.gameObject.SetActive(false);
        });

        //ユーザ情報ページボタンを押した時の処理を行う
        settingButton.onClick.AddListener(() =>
        {
            settingPage.SetActive(true);
            this.gameObject.SetActive(false);
        });

        //ユーザ情報ページボタンを押した時の処理を行う
        collectionButton.onClick.AddListener(() =>
        {
            collectionPage.SetActive(true);
            this.gameObject.SetActive(false);
        });

        //ユーザ情報ページボタンを押した時の処理を行う
        achieveButton.onClick.AddListener(() =>
        {
            achievePage.SetActive(true);
            this.gameObject.SetActive(false);
        });

        userDataManager.level
            .Subscribe(x => levelText.text = x.ToString())
            .AddTo(this);

        userDataManager.level
            .Where(value => value == 10)
            .Subscribe(_ => Charactor.sprite = Normal)
            .AddTo(this);

        userDataManager.level
            .Where(value => value == 20)
            .Subscribe(_ => Charactor.sprite = Mukimuki)
            .AddTo(this);
        userDataManager.exp
            .Subscribe(x =>
            {
                expSlider.value = (float)x / (float)userDataManager.CalculateRequiredExp();
            })
            .AddTo(this);
    }

    private void OnEnable()
    {
        if (userDataManager.isOperate)
            moveButton.SetActive(true);
        else
            moveButton.SetActive(false);
    }
}
