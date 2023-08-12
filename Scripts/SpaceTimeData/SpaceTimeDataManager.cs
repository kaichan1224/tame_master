using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 時空間情報処理のためのクラス
/// </summary>
public class SpaceTimeDataManager : MonoBehaviour
{
    // データを格納しているファイルのキー
    private const string SPACETIMEDATA_PREF_KEY = "SpaceTimeData";
    public SpaceTimeData spaceTimeData;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        Load(GetYYMMDD());
    }

    /// <summary>
    /// 指定した日付のデータをロードする
    /// </summary>
    /// <param name="yymmdd">yymdd形式での日付</param>
    public void Load(string yymmdd)
    {
        if (PlayerPrefs.HasKey(SPACETIMEDATA_PREF_KEY+yymmdd))
        {
            string loadjson = PlayerPrefs.GetString(SPACETIMEDATA_PREF_KEY+yymmdd);
            spaceTimeData = JsonUtility.FromJson<SpaceTimeData>(loadjson);
        }
        else
        {
            spaceTimeData= new SpaceTimeData();
            spaceTimeData.dataList= new List<SpaceTimeOneData>();
        }
    }

    /// <summary>
    /// 指定した日付のデータを保存する
    /// </summary>
    /// <param name="yymmdd">yymmdd形式の日付</param>
    private void Save(string yymmdd)
    {
        string savejson = JsonUtility.ToJson(spaceTimeData);
        PlayerPrefs.SetString(SPACETIMEDATA_PREF_KEY+yymmdd, savejson);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 当日のデータに位置情報データを追加する
    /// </summary>
    /// <param name="addData"></param>
    public void AddData(SpaceTimeOneData addData)
    {
        spaceTimeData.dataList.Add(addData);
    }

    /// <summary>
    /// YYMMDD形式で今日の日付を取得する
    /// </summary>
    /// <returns></returns>
    public static string GetYYMMDD()
    {
        DateTime currentDate = DateTime.Today;
        string dateString = currentDate.ToString("yyyy-MM-dd");
        return dateString;
    }

    /// <summary>
    /// アプリを落とした時の処理
    /// </summary>
    private void OnDestroy()
    {
        Save(GetYYMMDD());
    }
}
