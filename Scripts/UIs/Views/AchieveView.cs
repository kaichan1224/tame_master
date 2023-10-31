using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class AchieveView : MonoBehaviour
{
    //アチーブのプレハブ
    [SerializeField] private GameObject achieveUIPrefab;
    [SerializeField] private Button backButton;
    //表示するアイコンを格納するリスト
    private List<GameObject> achieveList = new List<GameObject>();
    //動物アイコンを表示するRectTransform
    [SerializeField] private RectTransform contentRectTransform;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 表示されているアチーブメントを全て削除する
    /// </summary>
    public void HideAchieves()
    {
        //リストの中身を削除
        foreach (var obj in achieveList)
        {
            obj.Destroy();
        }
        achieveList.Clear();
    }

    /// <summary>
    /// 指定したリストのアチーブメントを表示させる
    /// </summary>
    /// <param name="ownedAnimalDatas"></param>
    public void ShowAchieves(List<AchievemParam> achieves,List<Sprite> sprites)
    {
        //所有済み動物データを元に個数分アイコンを生成する
        for (int i = 0; i < achieves.Count; i++)
        {
            var obj = Instantiate(achieveUIPrefab,contentRectTransform);
            obj.GetComponent<AchieveButton>().Init(achieves[i],sprites[i]);
            achieveList.Add(obj);
        }
    }

    /// <summary>
    /// 指定した名前のアチーブメントを更新する
    /// </summary>
    /// <param name="achieveName">更新するアチーブの名前</param>
    /// <param name="achievemParam">更新データ</param>
    public void UpdateAchieve(string achieveName,AchievemParam achievemParam)
    {

    }
}


