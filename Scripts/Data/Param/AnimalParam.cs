/**************************************
 *** Designer:AL21115
 *** Date:2023.7.1
 *** 動物のデータクラス
 **************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 動物に関する情報が入ったデータクラス
/// スプライトについては辞書としてAnimalSpritMasterDataに名前と対応させて持たせてある。
/// </summary>
[Serializable]
public class AnimalParam
{
    public ANIMAL_NAME name;
    public ANIMAL_RANK rank;
    public StatusParam status;
    public ActionType actionType1;
    public ActionType actionType2;
    public string explain;
    public bool isParty = false;
    //動物のランク
    public enum ANIMAL_RANK
    {
        None = 0,
        S = 15,
        A = 50,
        B = 75,
        C = 100,
    }

    //動物の名前
    public enum ANIMAL_NAME
    {
        None,
        青狼,
        アリクイ,
        アルパカ,
        雌牛,
        豚,
        銀猪,
        ゴリラ,
        蛇,
        羊,
        犬,
        カエル,
        金熊,
        蜘蛛,
        緑鹿,
        翠狸,
        ヴェラキラプトル,
        アパトサウルス,
        鼠,
        パグ,
        ライオン,
        サイ,
        チンパンジー,
        柴犬,
        鹿,
        シマウマ,
        白馬,
        ステゴサウルス,
        ティラノサウルス,
        ミニタイガー,
        タイガー,
        トリケラトプス,
        兎,
        牛,
        狼,
        象,
    }
}
