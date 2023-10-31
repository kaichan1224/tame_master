using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ステータスのパラメータ
/// </summary>
[Serializable]
public class StatusParam
{
    public StatusParam(int Hp,int Attack,int Defense,int Speed)
    {
        this.Hp = Hp;
        this.Attack = Attack;
        this.Defense = Defense;
        this.Speed = Speed;
    }
    public int Hp;
    public int Attack;
    public int Defense;
    public int Speed;

    public static StatusParam operator +(StatusParam a, StatusParam b)
    {
        return new StatusParam(a.Hp + b.Hp, a.Attack + b.Attack, a.Defense + b.Defense, a.Speed + b.Speed);
    }
}
