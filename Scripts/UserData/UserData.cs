using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    //移動距離
    public double distanceTraveled;
    //総消費kcal
    public double totalKcal;
    //現在のexp
    public int exp;
    //現在のレベル
    public int level;
    //テイムした動物の数
    public int tameCnt;
    //次回経験値を獲得するのに必要な移動距離
    public double nextRequiredDistance;
    //既にNormalの見た目に変化したかどうかのフラグ
    public bool isNormal;
    //既にMukimukiの見た目に変化したかどうかのフラグ
    public bool isMukimuki;
    //キャラクターの名前
    public string name;
    //オペレートモード
    public bool isOperate;
    //キャラクターを捕まえたかどうかの配列
    public int[] getAnimalList;
    public int tameSort;
}
