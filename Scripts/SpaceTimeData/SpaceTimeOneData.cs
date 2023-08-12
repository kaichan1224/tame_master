using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 位置情報の一つ当たりのデータクラス
/// </summary>
[Serializable]
public class SpaceTimeOneData
{
    public Vector3 position;
    public SpaceTimeOneData(Vector3 position)
    {
        this.position = position;
    }
}
