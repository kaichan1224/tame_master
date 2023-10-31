using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemParam
{
    public ItemType itemType;
    public ItemName name;
    public Rarity rarity;
    public StatusParam status;
    //ItemTypeが武器の時のみ使用
    public ActionType actionType1;
    public ActionType actionType2;
    public enum ItemType
    {
        None,
        武器,
        防具,
        アクセサリー,
    }
    public enum Rarity
    {
        N = 100,
        R = 20,
        SR = 5,
    }
    public enum ItemName
    {
        None,
        //N武器
        木の剣,
        //R武器
        銀の剣,
        //SR武器
        神聖なる大槌,
        //N防具
        木の盾,
        //R防具
        虎の鎧,
        //SR防具
        ドラゴンの鱗の鎧,
        //Nアクセサリー
        革の腕輪,
        //Rアクセサリー
        魔法の指輪,
        //SRアクセサリー
        伝説の首飾り,
    }
}
