using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

/// <summary>
/// マップ上に出現する動物を管理するモジュール
/// </summary>
public class Animal: MonoBehaviour
{
    //テイムマネージャ
    public TamingManager tamingManager;
    //キャラクターがクリックされたかどうか
    private bool isClicked = false;
    public AnimalData parameter;

    /// <summary>
    /// 動物をクリックした時に実行されるメソッド
    /// </summary>
    public void Action()
    {
        if (isClicked)
            return;
        tamingManager.StartTame(this.gameObject);
        isClicked = true;
    }
}
