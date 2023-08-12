using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 捕獲した動物を表示するためのクラス
/// </summary>
public class CollectionPageManager : MonoBehaviour
{
    //動物アイコンを表示する場所
    public RectTransform contentRectTransform;
    //閉じるボタン
    [SerializeField] private Button exitButton;
    //表示する時のアイコンのプレハブ
    [SerializeField] private GameObject animalButtonPrefab;
    //3dマップのページ
    [SerializeField] private GameObject map3dPage;
    //所有済みの動物データを管理するクラス
    [SerializeField] private OwnedAnimalDataManager ownedAnimalDataManager;
    //表示するアイコンを格納するリスト
    private List<GameObject> displayAnimalButtonList = new List<GameObject>();

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            map3dPage.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// ページ表示時の処理
    /// </summary>
    private void OnEnable()
    {
        //リストの中身を削除
        foreach (var obj in displayAnimalButtonList)
        {
            obj.Destroy();
        }
        displayAnimalButtonList.Clear();
        //所有済み動物データを元に個数分アイコンを生成する
        for (int i = 0; i < ownedAnimalDataManager.ownedAnimalData.dataList.Count; i++)
        {
            var obj = Instantiate(animalButtonPrefab, contentRectTransform);
            obj.GetComponent<AnimalButton>().Init(ownedAnimalDataManager.ownedAnimalData.dataList[i]);
            displayAnimalButtonList.Add(obj);
        }
    }
}
