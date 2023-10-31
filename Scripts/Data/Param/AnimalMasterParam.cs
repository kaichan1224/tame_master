using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 動物の名前とスプライトとゲームオブジェクトのセット(マスタデータ管理用)
/// </summary>

[Serializable]
public class AnimalMasterParam
{
    public AnimalParam.ANIMAL_NAME name;
    public Animal animal;
    public Sprite image;
}
