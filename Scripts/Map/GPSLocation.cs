/**************************************
 *** Designer:AL21115
 *** Date:2023.7.1
 *** bgmを再生するためのモジュール
 *** Last Editor:AL21115
 *** Last Edited Date:2023.7.5
 **************************************/
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>位置情報を簡易に取得するstaticクラス。例外処理を考慮すること</summary>
public static class GPSLocation
{
    public static Location location { get; private set; }
    static bool isOnTask = false;
    public static async UniTask<Location> GetLocation(int tryNum = 6)
    {
        var isSuceeded = false;
        //一時的な取得失敗の可能性もあるため,指定回数取得してみる
        int count = 0;
        string exceptionText = "";
        while (!isSuceeded)
        {
            try
            {
                location = await GetLocationCycle();
                isSuceeded = true;
            }
            catch (Exception e)
            {
                count++;
                exceptionText += e;
                exceptionText += ", ";
                if (count > 6) break;
            }
        }
        if (!isSuceeded)
        {
            throw new Exception(exceptionText);
        }
        return location;
    }
    static async UniTask<Location> GetLocationCycle()
    {
        if (location == null) location = new Location();
        if (isOnTask)
        {
            throw new Exception("This Task may be going on double.");
        }
        else
        {
            isOnTask = true;
        }

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            isOnTask = false;
            throw new Exception("GPS is not enabled");
        }

        // Start service before querying location
        Input.location.Start();
        var count = 0f;
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            Debug.Log(Input.location.status + ", time count = " + count);
            if (count > 20f)
            {
                Input.location.Stop();
                isOnTask = false;
                throw new Exception("Time Out!");
            }
            count += Time.deltaTime;
            await UniTask.Yield();
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Input.location.Stop();
            isOnTask = false;
            throw new Exception("Unable to determine device location");
        }

        // Debug.Log(Input.location.status);
        location.Latitude = Input.location.lastData.latitude;
        location.Longitude = Input.location.lastData.longitude;
        Input.location.Stop();
        isOnTask = false;
        return location;
    }
}

