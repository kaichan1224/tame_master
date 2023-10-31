/**************************************
 *** Designer:AL21115
 *** Date:2023.5.23
 *** ユーザ情報のデータクラス
 *** Last Editor:AL21115
 *** Last Edited Date:2023.6.10
 **************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーが持つパラメタ。セーブ時に使用。その他設定項目等も。
/// TODO:UserParamを増やしたら、UserDataModelも変更する必要あり
/// </summary>
[Serializable]
public class UserParam
{
    //移動距離
    public double distanceTraveled;
    //総消費kcal
    public double totalKcal;
    //現在のexp
    public int exp;
    //現在のレベル
    public int level;
    //今までテイムした動物の数
    public int tameCnt;
    //キャラクターの名前
    public string name;
    //所持している動物
    public List<AnimalParam> ownedAnimals;
    //所持しているアイテム
    public List<ItemParam> ownedItems;
    //パーティー
    public PartyParam party;
    //今現在のアチーブメント(TODO:1日置きで更新)
    public List<AchievemParam> ownedAchieves;
    //プレイヤーのスタッツ
    public StatusParam playerStatus;
    //持っているガチャチケットの数
    public int gachaTicketNum;
    //最終ログイン時間
    public string latestAchieveUpdateDay;
    //チュートリアルを完了したかどうか
    public bool isFinishTutorial;
    //木の実の個数
    public int kinomiNum;

    public UserParam(double distanceTraveled,
        double totalKcal,
        int exp, int level,
        int tameCnt, string name,
        List<AnimalParam> ownedAnimals,
        List<ItemParam> ownedItems,PartyParam party,
        List<AchievemParam> ownedAchieves,
        StatusParam playerStatus,
        int gachaTicketNum,
        string latestAchieveUpdateDay,
        bool isFinishTutorial,
        int kinomiNum
        )
        {
        this.distanceTraveled = distanceTraveled;
        this.totalKcal = totalKcal;
        this.exp = exp;
        this.level = level;
        this.tameCnt = tameCnt;
        this.name = name;
        this.ownedAnimals = ownedAnimals;
        this.ownedItems = ownedItems;
        this.party = party;
        this.ownedAchieves = ownedAchieves;
        this.playerStatus = playerStatus;
        this.gachaTicketNum = gachaTicketNum;
        this.latestAchieveUpdateDay = latestAchieveUpdateDay;
        this.isFinishTutorial = isFinishTutorial;
        this.kinomiNum = kinomiNum;
        }
}
