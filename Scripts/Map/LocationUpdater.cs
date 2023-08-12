using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 位置情報更新のためのクラス
/// </summary>
public class LocationUpdater : MonoBehaviour
{
    //位置情報の取得間隔
    [SerializeField] private int intervalTime = 5;
    //経度
    public float latitude = 35.6894f;
    //緯度
    public float longitude = 139.6917f;
    private async void Start()
    {
        while (true)
        {
            try
            {
                Location location = await GPSLocation.GetLocation();
                latitude = (float)location.Latitude;
                longitude = (float)location.Longitude;
            }
            catch (Exception e)
            {
                Debug.Log("Failed to get location: " + e.Message);
            }
            // 指定時間待機
            await UniTask.Delay(TimeSpan.FromSeconds(intervalTime));
        }
    }
}

