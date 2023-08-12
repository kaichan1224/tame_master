using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;

/// <summary>
/// ユーザー情報を管理する
/// </summary>
public class UserDataManager : MonoBehaviour
{
    private const string USERDATA_PREF_KEY = "UserData";
    public UserData userData;
    public ReactiveProperty<double> distanceTraveled = new();
    public ReactiveProperty<int> exp = new();
    public ReactiveProperty<int> level = new();
    public ReactiveProperty<double> totalKcal = new();
    public ReactiveProperty<bool> isMukimuki = new();
    public ReactiveProperty<bool> isNormal = new();
    public ReactiveProperty<int> tameSort = new();
    public string playerName;
    public bool isOperate;
    /// <summary>
    /// 最初にデータをロードするメソッドを呼ぶ
    /// </summary>
    private void Awake()
    {
        Load();
    }

    private void Update()
    {
        if (distanceTraveled.Value >= userData.nextRequiredDistance)
        {
            UpdateExp(10);
            userData.nextRequiredDistance++;
        }
    }

    /// <summary>
    /// データをロードするメソッド.データが存在しなければ新規作成する
    /// </summary>
    public void Load()
    {
        if (PlayerPrefs.HasKey(USERDATA_PREF_KEY))
        {
            string loadjson = PlayerPrefs.GetString(USERDATA_PREF_KEY);
            userData = JsonUtility.FromJson<UserData>(loadjson);
        }
        else
        {
            userData = new UserData();
            userData.distanceTraveled = 0;
            userData.exp = 0;
            userData.level = 1;
            userData.totalKcal = 0f;
            userData.nextRequiredDistance = 1d;
            userData.isMukimuki = false;
            userData.isNormal = false;
            userData.name = "ガリ男";
            userData.isOperate = false;
            userData.getAnimalList = new int[46];
            userData.tameCnt = 0;
            userData.tameSort = 0;
        }
        SetReactiveProperties();
    }

    /// <summary>
    /// ReactivePropertyの各変数の値のロードしたユーザデータからロードする
    /// </summary>
    public void SetReactiveProperties()
    {
        distanceTraveled.Value = userData.distanceTraveled;
        exp.Value = userData.exp;
        level.Value = userData.level;
        totalKcal.Value = userData.totalKcal;
        isNormal.Value = userData.isNormal;
        isMukimuki.Value = userData.isMukimuki;
        playerName = userData.name;
        isOperate = userData.isOperate;
        tameSort.Value = userData.tameSort;
    }


    /// <summary>
    /// データをセーブするメソッド
    /// </summary>
    public void Save()
    {
        string savejson = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(USERDATA_PREF_KEY, savejson);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 距離を増加させるメソッド
    /// </summary>
    /// <param name="moveDistance">増加する</param>
    public void UpdateDistanceTraveled(double moveDistance)
    {
        distanceTraveled.Value += moveDistance;
        totalKcal.Value += CalculateCaloriesBurned(moveDistance);
    }
    /// <summary>
    /// 消費カロリーを計算するメソッド
    /// </summary>
    /// <returns></returns>
    public double CalculateCaloriesBurned(double moveDistance)
    {
        double weight = 70d;
        // 消費カロリーの計算式（例としてMET（代謝当量）を使用）
        // MET値は活動の強度によって異なる値を使用する必要があります
        // ここでは簡略化のために一定のMET値を使用しています
        double metValue = 3.5d;  // 適切なMET値を設定してください
        // 消費カロリーの計算
        double calories = metValue * weight * moveDistance/ 1000f;

        return calories;
    }

    public void UpdateTameInfo(int tameId)
    {
        userData.tameCnt++;
    }

    /// <summary>
    /// 経験値を増加させるメソッド
    /// 一定量を超えたらレベルも増加する
    /// </summary>
    /// <param name="addExp">増加する経験値の量:50ぐらいがいいかも</param>
    public void UpdateExp(int addExp)
    {
        exp.Value += addExp;
        int requiredExp = CalculateRequiredExp();
        if (exp.Value >= requiredExp)
        {
            level.Value++;
            exp.Value = exp.Value - requiredExp;
        }
    }

    public int CalculateRequiredExp()
    {
        // レベルアップに必要な経験値の計算ロジックを記述する
        // 例: 必要な経験値 = レベル * 100
        return level.Value * 100;
    }

    /// <summary>
    /// 総kcalを計算するクラス
    /// </summary>
    /// <param name="moveDistance"></param>
    public void UpdatetotalKcal(float addKcal)
    {
        totalKcal.Value += addKcal;
    }

    private void OnDestroy()
    {
        userData.distanceTraveled  = distanceTraveled.Value;
        userData.exp = exp.Value;
        userData.level = level.Value;
        userData.totalKcal = totalKcal.Value;
        userData.isNormal = isNormal.Value;
        userData.isMukimuki = isMukimuki.Value;
        userData.name = playerName;
        userData.isOperate = isOperate;
        userData.tameSort = tameSort.Value;
        Save();
    }
}
