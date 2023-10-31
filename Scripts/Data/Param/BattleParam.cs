using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 対人バトル時に使用するパラメータ
/// キャラクター一人が持つパラメータ
/// </summary>
[Serializable]
public class BattleParam
{
    //パラメータ
    public StatusParam status;
    //攻撃1
    public ActionType actionType1;
    //攻撃2
    public ActionType actionType2;
    //敵か味方かどうか
    public PlayerType playerType;
    //選択した攻撃のデータ
    public ActionType selectActionType;
    //ゲームオブジェクト作成のためのパラメータ(view用)
    public AnimalParam animalParam;
    //バトル時に登録された順番(0~2)
    public int order;
    public enum PlayerType
    {
        Player,
        Enemy
    }

    public BattleParam(PartyParam party,StatusParam playerStatus,PlayerType type,int order)
    {
        actionType1 = party.buki.actionType1;
        actionType2 = party.buki.actionType2;
        status = playerStatus;
        //アイテム文のパラメータ上昇
        status += party.akuse.status;
        status += party.buki.status;
        status += party.bougu.status;
        playerType = type;
        this.order = order;
    }

    public BattleParam(AnimalParam animalParam,PlayerType type,int order)
    {
        status = animalParam.status;
        actionType1 = animalParam.actionType1;
        actionType2 = animalParam.actionType2;
        playerType = type;
        this.animalParam = animalParam;
        this.order = order;
    }
}
