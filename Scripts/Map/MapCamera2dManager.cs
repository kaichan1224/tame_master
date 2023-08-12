using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マップを移動するためのモジュール
/// </summary>
public class MapCamera2dManager : MonoBehaviour
{
    //　プレイヤーのtransformをインスペクターから取得する
    [SerializeField] private Transform player;

    private Vector2 touchStartPos;

    /// <summary>
    /// マップの場所を移動するためのモジュール
    /// </summary>
    private void Update()
    {
        // タッチされている指の数を取得
        int touchCount = Input.touchCount;

        // スワイプとピンチアウトの処理
        if (touchCount == 1)
        {
            // スワイプ処理
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStartPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.GetTouch(0).deltaPosition;
                transform.Translate(-delta * Time.deltaTime * 5);
            }
        }
    }
}

