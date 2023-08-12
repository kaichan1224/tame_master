using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アチーブメントに関するデータクラス
[Serializable]
public class Achievement
{
    //アチーブメントの名前
    public string name;
    //説明
    public string description;
    //アイコン
    public Sprite icon;
    //現在の進捗率
    public int currentprogress;
    //進捗率の上限
    public int maxprogress;
    //達成したかどうか
    public bool isAchieved;
    public string unit;

    // コンストラクタ
    public Achievement(string name, string description, Sprite icon)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        isAchieved = false;
    }
}

