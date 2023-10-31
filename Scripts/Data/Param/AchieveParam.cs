/**************************************
 *** Date:2023.5.23
 *** 達成項目のデータクラス
 *** Last Editor:AL21115
 *** Last Edited:2023.6.8
 **************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アチーブメントに関するデータクラス
[Serializable]
public class AchievemParam
{
    //アチーブメントの名前
    public AchieveType achieveType;
    //現在の進捗率
    public int currentprogress;
    //進捗率の上限
    public int maxprogress;
    //達成したかどうか
    public bool isAchieved;
    //アチーブメントを達成したときの報酬
    public RewardType rewardType;
    public enum AchieveType
    {
        動物を1体捕まえる,
        動物を3体捕まえる,
        動物を5体捕まえる,
        動物を10体捕まえる,
        移動距離1km,
        移動距離5km,
        移動距離10km,
        バトルで1回勝つ,
        バトルで2回勝つ,
        バトルで3回勝つ,
    }
    public enum RewardType
    {
        ガチャチケ1枚,
        ガチャチケ3枚,
        ガチャチケ5枚,
        経験値100,
        経験値300,
        経験値500,
        木の実3個
    }
}

