/**************************************
 *** Designer:AL21115
 *** Date:2023.7.1
 *** 位置情報を更新するためのモジュール
 *** Last Editor:AL21115
 *** Last Edited Date:2023.7.5
 **************************************/
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 位置情報更新のためのクラス
/// </summary>
public class LocationUpdater : MonoBehaviour
{
    private int intervalTime;
    //経度
    public double latitude = 35.660421f;
    //緯度
    public double longitude = 139.795413f;
    public void SetIntervalTime(int time)
    {
        intervalTime = time;
    }
    public int IntervalTime => intervalTime;
    private async void Start()
    {
        SetIntervalTime(1);
        while (true)
        {
            try
            {
                Location location = await GPSLocation.GetLocation();
                latitude = location.Latitude;
                longitude = location.Longitude;
            }
            catch (Exception e)
            {
                //Debug.Log("Failed to get location: " + e.Message);
            }
            // 指定時間待機
            await UniTask.Delay(TimeSpan.FromSeconds(intervalTime));
        }
    }
}

