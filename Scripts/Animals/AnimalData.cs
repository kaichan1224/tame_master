using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 動物に関する情報が入ったデータクラス
/// </summary>
[Serializable]
public class AnimalData
{
    public int id;
    public string name;
    public Sprite sprite;
    public ANIMAL_RANK rank;
    public int hp;//キャラクターの体力
    //動物のランク
    public enum ANIMAL_RANK
    {
        GARIGARI,
        NORMAL,
        MUKIMUKI,
    }
}
