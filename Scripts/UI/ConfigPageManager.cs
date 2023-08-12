using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定画面におけるUIの処理を行うモジュール
/// TODO 位置情報の取得間隔等の修正をこの画面でできるようにする
/// </summary>
public class ConfigPageManager : MonoBehaviour
{
    [SerializeField] private UserDataManager userDataManager;
    //閉じるボタン
    [SerializeField] private Button closeButton;
    //3dマップのページ
    [SerializeField] private GameObject map3dPage;
    //操作モードOn/Off
    [SerializeField] private Toggle operateToggle;
    /// <summary>
    /// 設定画面がオンになった時の処理
    /// </summary>
    private void Start()
    {
        //閉じるボタンを押した時の処理
        closeButton.onClick.AddListener(() =>
        {
            map3dPage.SetActive(true);
            this.gameObject.SetActive(false);
        });
        operateToggle.isOn = userDataManager.isOperate;
        operateToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool value)
    {
        userDataManager.isOperate = value;
    }
}