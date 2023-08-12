using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 2dマップページにおけるUIの処理を行うモジュール
/// </summary>
public class Map2dPageManager : MonoBehaviour
{
    //閉じるボタン
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resetPlaceButton;
    //3dマップのページ
    [SerializeField] private GameObject map3dPage;
    //2dマップを表示するカメラ
    [SerializeField] private GameObject camera2d;
    //3dマップを表示するカメラ
    [SerializeField] private GameObject camera3d;
    //2dマップ
    [SerializeField] private GameObject map2d;
    //3dマップ
    [SerializeField] private GameObject map3d;
    [SerializeField] private GameObject player;
    /// <summary>
    /// 2dマップページを起動した時に処理を行うメソッド
    /// </summary>
    private void Start()
    {
        //マップスイッチボタンを押された時の処理を行う
        exitButton.onClick.AddListener(() =>
        {
            map3dPage.SetActive(true);
            camera3d.SetActive(true);
            camera2d.SetActive(false);
            map3d.SetActive(true);
            map2d.SetActive(false);
            this.gameObject.SetActive(false);
        });

        resetPlaceButton.onClick.AddListener(() =>
        {
            camera2d.transform.position = new Vector3(player.transform.position.x, camera2d.transform.position.y, player.transform.position.z);
        });
    }

    private void OnEnable()
    {
        camera2d.transform.position = new Vector3(player.transform.position.x, camera2d.transform.position.y, player.transform.position.z);
    }
}

