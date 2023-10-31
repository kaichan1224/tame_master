/**************************************
 *** Designer:AL21115
 *** Date:2023.7.1
 *** Purpose:動物のテイムが始まるための処理及び各動物の情報を保持するクラス
 *** Last Editor:AL21115
 *** Last Edited Date:2023.7.4
 **************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

/// <summary>
/// 動物のPrefabにアタッチする
/// マップ上に出現する動物に触れたかどうかを判定する
/// </summary>
public class AnimalCollider: MonoBehaviour
{
    //キャラクターがクリックされたかどうか
    public ReactiveProperty<bool> isClicked = new(false);
    /// <summary>
    /// 動物をクリックした時に実行されるメソッド
    /// </summary>
    public void Action()
    {
        isClicked.Value = true;
    }
}
