using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 行動アクションがもつパラメータ
/// </summary>
[Serializable]
public class ActionParam
{
    public ActionParam(int skillAttack,double hitRate,double criticalRate)
    {
        this.skillAttack = skillAttack;
        this.hitRate = hitRate;
        this.criticalRate = criticalRate;
    }
    //攻撃力
    public int skillAttack;
    //命中率
    public double hitRate;
    //会心率
    public double criticalRate;
    //回復量
    public int healValue;
}
