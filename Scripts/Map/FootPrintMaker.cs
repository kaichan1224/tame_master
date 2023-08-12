using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Map;
/// <summary>
/// 足跡生成処理を行うモジュールクラス。
/// </summary>
public class FootPrintMaker : MonoBehaviour
{
    //　プレイヤーの移動距離
    [SerializeField] private Transform player;
    // 足跡生成間隔　TODO 設定からいじれるようにする
    [SerializeField] private float intervalTime = 1f;
    // 3Dマップのゲームオブジェクト：3DページではLineRendererを非表示にするため、今のページが3Dか2Dを判別する必要がある。
    [SerializeField] private GameObject map3dObject;
    //　時空間情報管理部
    [SerializeField] private SpaceTimeDataManager spaceTimeDataManager;
    [SerializeField] private AbstractMap map3d;
    // 前回のピン設置時間を格納する変数
    float lastPinPlacementTime = 0.0f;
    //足跡が表示する位置の調整
    [SerializeField] private float adjust = 0f;
    // 2dマップに置いて通った場所を表示するために使用するLineRenderer
    public LineRenderer line;

    /// <summary>
    /// 足跡生成の初期化を行うメソッド
    /// </summary>
    private void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        //点の数
        line.positionCount = 0;
        //始点と終点を繋げない
        line.loop = false;
        //開始点の太さ
        line.startWidth = 1f;
        //終点の太さ
        line.endWidth = 1f;
        //取得されたデータ
        foreach (SpaceTimeOneData data in spaceTimeDataManager.spaceTimeData.dataList)
        {
            Vector3 pinPos = data.position;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, pinPos);
        }
    }

    /// <summary>
    /// 新たに足跡を生成する処理を行うメソッド. TODO 非同期処理で実装してもいいかも
    /// </summary>
    void Update()
    {
        //3Dマップの場面では、線を非表示にするための線の太さを0にする
        if (map3dObject.activeSelf)
        {
            line.startWidth = 0f; // 線の幅をゼロに設定
            line.endWidth = 0f; // 線の幅をゼロに設定
        }
        //そうでない時は線の太さを元に戻す
        else
        {
            line.startWidth = 1f; // 線の幅をゼロに設定
            line.endWidth = 1f; // 線の幅をゼロに設定
        }
        // ピンを設置する時間かどうか確認する
        if (Time.time - lastPinPlacementTime < intervalTime)
            return;
        // プレイヤーの位置にピンを設置する
        Vector3 position = player.position;
        //線を描写する部分
        line.positionCount++;
        line.SetPosition(line.positionCount-1, new Vector3(position.x, position.y +adjust, position.z));
        // 前回のピン設置時間を更新する
        lastPinPlacementTime = Time.time;
        //TODO　確認用
        spaceTimeDataManager.AddData(new SpaceTimeOneData(position));
    }
}
