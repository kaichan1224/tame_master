using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using System.Linq;
using System;

/// <summary>
/// アチーブメントに関する情報を扱い、数値の計算等及び更新を行うクラス
/// </summary>
public class AchieveModel
{
    private AchieveMasterData achieveMasterData;
    private UserDataModel userDataModel;

    [Inject]
    public AchieveModel(AchieveMasterData achieveMasterData, UserDataModel userDataModel)
    {
        this.achieveMasterData = achieveMasterData;
        this.userDataModel = userDataModel;
    }

    /// <summary>
    /// アチーブを更新した日付を記録
    /// </summary>
    public void SetLatestAchieveUpdateDay()
    {
        userDataModel.SetUpdateDay(DateTimeString(System.DateTime.Now));
    }



    public void SetAchieve()
    {
        var flag = IsResetAchieve();
        Debug.Log(flag);
        if (flag)
        {
            PublishAchieve(UnityEngine.Random.Range(3,6));
        }
    }

    /// <summary>
    /// アチーブメントの更新するのかどうか
    /// </summary>
    public bool IsResetAchieve()
    {
        string today = DateTimeString(System.DateTime.Now);
        Debug.Log($"{today},{userDataModel.latestAchieveUpdateDay}");
        if (today == userDataModel.latestAchieveUpdateDay)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string DateTimeString(DateTime date)
    {
        return date.Year.ToString() + "/" + date.Month.ToString() + "/" + date.Day.ToString();
    }

    /// <summary>
    /// 指定した数の分だけミッションを発行する
    /// </summary>
    public void PublishAchieve(int cnt)
    {
        //シャッフル
        achieveMasterData.datas.ShuffleList();
        userDataModel.SetAchieves(achieveMasterData.datas.Take(3).ToList());
    }

    /// <summary>
    /// 指定したアチーブメントタイプの報酬をプレイヤーが所有しているアチーブから探し、もし存在するなら更新する
    /// </summary>
    /// <param name="achieveType"></param>
    public void UpdateAchieve(AchievemParam.AchieveType achieveType,int deltaAmount)
    {
        userDataModel.UpdateAchieve(achieveType,deltaAmount);
    }

    /// <summary>
    /// 敵を捕まえた時に呼ぶメソッド
    /// </summary>
    public void UpdateTameAnimalNumAchieve()
    {
        UpdateAchieve(AchievemParam.AchieveType.動物を1体捕まえる,1);
        UpdateAchieve(AchievemParam.AchieveType.動物を3体捕まえる, 1);
        UpdateAchieve(AchievemParam.AchieveType.動物を5体捕まえる, 1);
        UpdateAchieve(AchievemParam.AchieveType.動物を10体捕まえる, 1);
    }

    /// <summary>
    /// バトルに勝利した時に呼ぶメソッド
    /// </summary>
    public void UpdateBattleWinAchieve()
    {
        UpdateAchieve(AchievemParam.AchieveType.バトルで1回勝つ,1);
        UpdateAchieve(AchievemParam.AchieveType.バトルで2回勝つ,1);
        UpdateAchieve(AchievemParam.AchieveType.バトルで3回勝つ, 1);
    }

}
