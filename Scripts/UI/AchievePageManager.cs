using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// アチーブメントページにおけるUI処理をおこなうモジュール
/// </summary>
public class AchievePageManager : MonoBehaviour
{
    //3dマップページ
    [SerializeField] private GameObject map3dPage;
    //閉じるボタン
    [SerializeField] private Button backButton;
    //アチーブメントページが表示された時に実行される初期化メソッド
    private void Start()
    {
        //閉じるボタンが押された時の処理
        backButton.onClick.AddListener(() =>
        {
            map3dPage.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }
}
