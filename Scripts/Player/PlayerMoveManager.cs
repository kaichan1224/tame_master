using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Unity.Collections;

/// <summary>
/// プレイヤーが位置情報に基づいて移動する制御を行うクラス
/// TODO DataUpdateは実機の際はLonlatGetter内でデータを保存するようにするため、不要
/// </summary>
public class PlayerMoveManager : MonoBehaviour
{
    // 2dマップを管理するクラス
    [SerializeField] private AbstractMap map2d;
    // 3dマップを管理するクラス
    [SerializeField] private AbstractMap map3d;
    //　位置情報を取得するためのクラス
    // [SerializeField] private LonlatGetter lonLatGetter;
    [SerializeField] private LocationUpdater locationUpdater;
    [SerializeField] private Transform body;

    public bool upButtonDownFlag = false;
    public bool rightButtonDownFlag = false;
    public bool leftButtonDownFlag = false;
    public bool downButtonDownFlag = false;


    //　キーボードで操作する際の移動スピード
    [SerializeField] private float speed = 2f;
    // ユーザーデータを管理するためのクラス
    [SerializeField] private UserDataManager userDataManager;
    // 距離を取得する間隔
    [SerializeField] private float intervalTime = 60f;
    //　直前の位置
    private Location prevLocation = new Location();
    //　今現在の位置
    private Location currentLocation = new Location();

    /// <summary>
    /// 移動した距離を記録する定期実行を開始するメソッド
    /// TODO　StartからOnEnableに変更したのを書類に書く
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(DataUpdate());
    }

    /// <summary>
    /// フレーム置きに実行されるメソッド
    /// </summary>
    private void Update()
    {
        var prevPosition = transform.position;
        //キーボード操作の時の移動方法
        if (userDataManager.isOperate)
        {
            OperateMove();
        }
        //スマホの位置情報に基づく移動の際に実行されるメソッド
        else
        {
            MoveByLonLat();
        }
        var position = transform.position;
        var delta = position - prevPosition;
        // 静止している状態だと、進行方向を特定できないため回転しない
        if (delta == Vector3.zero)
        {
            return;
        }
        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(delta, Vector3.up);
        // オブジェクトの回転に反映
        body.rotation = rotation;


    }

    /// <summary>
    /// オペレートモードの時に、キーボードの操作によりプレイヤーが移動するためのメソッド
    /// </summary>
    private void OperateMove()
    {
        //キーボード操作の場合の移動方法
        //WASDで移動する.
        if (upButtonDownFlag)
        {
            transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        }
        if (leftButtonDownFlag)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (downButtonDownFlag)
        {
            transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
        }
        if (rightButtonDownFlag)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
    }


    /// <summary>
    /// スマホの位置情報に基づいてプレイヤーが移動する処理を行うメソッド
    /// </summary>
    private void MoveByLonLat()
    {
        Vector3 mapPos;
        // map2dを用いて、緯度経度をゲームシーン上の座標に変換する
        // 表示されているマップのよって非アクティブかどうかが異なるため、アクティブになっているマップの方でメソッドを呼び出す必要がある
        if (map2d.gameObject.activeSelf)
        {
            mapPos = map2d.GeoToWorldPosition(new Vector2d(locationUpdater.latitude, locationUpdater.longitude), true);
        }
        else
        {
            mapPos = map3d.GeoToWorldPosition(new Vector2d(locationUpdater.latitude, locationUpdater.longitude), true);
        }
        //プレイヤーの場所を変更する
        transform.position = new Vector3(mapPos.x, 0.5f, mapPos.z);

    }

    /// <summary>
    /// 一定時間おきにプレイヤーの移動距離を計算し、ユーザーデータ管理部に反映させる.
    /// </summary>
    /// <returns>データを取得する時間の間隔</returns>
    IEnumerator DataUpdate()
    {

        while (true)
        {
            if (map2d.gameObject.activeSelf)
            {
                prevLocation.Latitude = map2d.WorldToGeoPosition(transform.position).x;
                prevLocation.Longitude = map2d.WorldToGeoPosition(transform.position).y;
            }
            else
            {
                prevLocation.Latitude = map3d.WorldToGeoPosition(transform.position).x;
                prevLocation.Longitude = map3d.WorldToGeoPosition(transform.position).y;
            }
            //intervalTimeだけ待機する
            yield return new WaitForSeconds(intervalTime);
            if (map2d.gameObject.activeSelf)
            {
                currentLocation.Latitude = map2d.WorldToGeoPosition(transform.position).x;
                currentLocation.Longitude = map2d.WorldToGeoPosition(transform.position).y;
            }
            else
            {
                currentLocation.Latitude = map3d.WorldToGeoPosition(transform.position).x;
                currentLocation.Longitude = map3d.WorldToGeoPosition(transform.position).y;
            }
            //intervalTimeの待機前と後の緯度経度を元に移動距離を測定する
            double addDistance = NaviMath.LatlngDistance(prevLocation, currentLocation);
            //移動距離が測れない時?Nanになるのでそうでない時は測定する
            if(!double.IsNaN(addDistance))
                userDataManager.UpdateDistanceTraveled(addDistance);
        }
    }

    //以下各ボタンが押された時とボタンから手を話した時のメソッド

    public void OnUpButtonDown()
    {
        upButtonDownFlag = true;
    }

    public void OnUpButtonUp()
    {
        upButtonDownFlag = false;
    }

    public void OnDownButtonDown()
    {
        downButtonDownFlag = true;
    }

    public void OnDownButtonUp()
    {
        downButtonDownFlag = false;
    }

    public void OnRigthButtonDown()
    {
        rightButtonDownFlag = true;
    }

    public void OnRightButtonUp()
    {
        rightButtonDownFlag = false;
    }

    public void OnLeftButtonDown()
    {
        leftButtonDownFlag = true;
    }

    public void OnLeftButtonUp()
    {
        leftButtonDownFlag = false;
    }
}
