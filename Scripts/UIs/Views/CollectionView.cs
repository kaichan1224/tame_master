using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class CollectionView : MonoBehaviour
{
    //閉じるボタン
    [SerializeField] private Button backButton;
    //表示する時のアイコンのプレハブ
    [SerializeField] private GameObject animalButtonPrefab;
    //表示するアイコンを格納するリスト
    private List<GameObject> displayAnimalButtonList = new List<GameObject>();
    //動物アイコンを表示するRectTransform
    [SerializeField] private RectTransform contentRectTransform;
    //動物の詳細パネル
    [SerializeField] private AnimalPanel animalPanel;
    public AnimalPanel AnimalPanel => animalPanel;
    public List<AnimalButton> animalButtons = new List<AnimalButton>();
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();

    /// <summary>
    /// 表示されている動物を全て削除する
    /// </summary>
    public void HideAnimals()
    {
        //リストの中身を削除
        foreach (var obj in displayAnimalButtonList)
        {
            obj.Destroy();
        }
        displayAnimalButtonList.Clear();
        animalButtons.Clear();
    }
    public void InitAnimalPanel(AnimalParam animalParam)
    {
        animalPanel.Init(animalParam);
    }

    public void ShowAnimalPanel()
    {
        animalPanel.Show();
    }

    public void HideAnimalPanel()
    {
        animalPanel.Hide();
    }

    /// <summary>
    /// 所有している動物を全て反映させる
    /// </summary>
    /// <param name="ownedAnimalDatas"></param>
    public void ShowAnimals(List<AnimalParam> ownedAnimalDatas, List<Sprite> sprites)
    {
        //所有済み動物データを元に個数分アイコンを生成する
        for (int i = 0; i < ownedAnimalDatas.Count; i++)
        {
            //TODO:AnimalButtonPrefabはGameObjectじゃなくてそのままAnimalButtonの型にすればいいのでは?
            var obj = Instantiate(animalButtonPrefab, contentRectTransform);
            var animalButton = obj.GetComponent<AnimalButton>();
            animalButton.Init(ownedAnimalDatas[i],sprites[i]);
            displayAnimalButtonList.Add(obj);
            animalButtons.Add(animalButton);
        }
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
