using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using TMPro;

public class PartyView : MonoBehaviour
{
    //閉じるボタン
    [SerializeField] private Button backButton;
    [SerializeField] private Button animal1Button;
    [SerializeField] private Button animal2Button;
    [SerializeField] private Button bukiButton;
    [SerializeField] private Button bouguButton;
    [SerializeField] private Button akuseButton;
    [SerializeField] private TMP_Text text;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public IObservable<Unit> OnAnimal1 => animal1Button.OnClickAsObservable();
    public IObservable<Unit> OnAnimal2 => animal2Button.OnClickAsObservable();
    public IObservable<Unit> OnBuki => bukiButton.OnClickAsObservable();
    public IObservable<Unit> OnBougu => bouguButton.OnClickAsObservable();
    public IObservable<Unit> OnAkuse => akuseButton.OnClickAsObservable();

    //表示する時のアイコンのプレハブ
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject animalButtonPrefab;
    //表示するアイコンを格納するリスト
    private List<GameObject> displayButtonList = new List<GameObject>();
    public List<ItemButton> itemButtons = new List<ItemButton>();
    public List<AnimalButton> animalButtons = new List<AnimalButton>();
    //動物アイコンを表示するRectTransform
    [SerializeField] private RectTransform contentRectTransform;
    public void SetAnimal1(Sprite animal1)
    {
        if(animal1 != null)
            animal1Button.image.sprite = animal1;
    }

    public void SetAnimal2(Sprite animal2)
    {
        if(animal2 != null)
            animal2Button.image.sprite = animal2;
    }

    public void SetBuki(Sprite buki)
    {
        if(buki != null)
            bukiButton.image.sprite = buki;
    }

    public void SetBougu(Sprite bougu)
    {
        if(bougu != null)
            bouguButton.image.sprite = bougu;
    }
    public void SetAkusesari(Sprite akusesari)
    {
        if(akusesari != null)
            akuseButton.image.sprite = akusesari;
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetText(String word)
    {
        text.text = word;
    }

    /// <summary>
    /// 表示されているアイテムを全て削除する
    /// </summary>
    public void HideItems()
    {
        //リストの中身を削除
        foreach (var obj in displayButtonList)
        {
            obj.Destroy();
        }
        displayButtonList.Clear();
        itemButtons.Clear();
    }

    /// <summary>
    /// 所有しているアイテムを全て反映させる
    /// </summary>
    /// <param name="ownedAnimalDatas"></param>
    public void ShowItems(List<ItemParam> ownedItems,List<Sprite> ownedItemSprites)
    {
        //所有済み動物データを元に個数分アイコンを生成する
        for (int i = 0; i < ownedItems.Count; i++)
        {
            var obj = Instantiate(itemButtonPrefab, contentRectTransform);
            //TODO:ボタンの初期化
            var itemButton = obj.GetComponent<ItemButton>();
            itemButton.Init(ownedItems[i],ownedItemSprites[i]);
            displayButtonList.Add(obj);
            itemButtons.Add(itemButton);
        }
    }

    /// <summary>
    /// 表示されている動物を全て削除する
    /// </summary>
    public void HideAnimals()
    {
        //リストの中身を削除
        foreach (var obj in displayButtonList)
        {
            obj.Destroy();
        }
        displayButtonList.Clear();
        //ボタン
        animalButtons.Clear();
    }

    /// <summary>
    /// 所有している動物を全て反映させる
    /// </summary>
    /// <param name="ownedAnimalDatas"></param>
    public void ShowAnimals(List<AnimalParam> ownedAnimalDatas,List<Sprite> ownedAnimalSprites)
    {
        //所有済み動物データを元に個数分アイコンを生成する
        for (int i = 0; i < ownedAnimalDatas.Count; i++)
        {
            var obj = Instantiate(animalButtonPrefab, contentRectTransform);
            var animalButton = obj.GetComponent<AnimalButton>();
            animalButton.Init(ownedAnimalDatas[i],ownedAnimalSprites[i]);
            //初期化
            if (ownedAnimalDatas[i].isParty)
                animalButton.SetActiveProhibitImage(true);
            displayButtonList.Add(obj);
            animalButtons.Add(animalButton);
        }
    }

}
