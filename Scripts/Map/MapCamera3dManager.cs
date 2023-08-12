using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3Dカメラの視点変更のためのクラス
/// </summary>
public class MapCamera3dManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float angle = 5f;
    [SerializeField] private UserDataManager userDataManager;
    // スワイプの方向
    private Vector2 swipeDirection;

    // スワイプの開始地点
    private Vector2 swipeStartPos;

    private void Update()
    {
        if (!userDataManager.isOperate)
            RotateCamera();
        else
        {
            transform.localPosition = new Vector3(0,9.57f,-8.28f);
            transform.localRotation = Quaternion.Euler(35, 0, 0);
        }
    }

    private void RotateCamera()
    {
        // タッチまたはマウスの入力を監視
        if (Input.GetMouseButtonDown(0))
        {
            // 入力の開始地点を記録
            swipeStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            //押された時の移動地点
            Vector2 swipeEndPos = Input.mousePosition;
            //x軸方向のスワイプ距離を求める
            float swipeDistance = swipeStartPos.x - swipeEndPos.x;
            if (swipeDistance > 5f)
            {
                transform.RotateAround(player.position, Vector3.up, angle);
            }
            else if (swipeDistance < -5f)
            {
                transform.RotateAround(player.position, Vector3.up, -angle);
            }
            // 入力の開始地点を記録
            swipeStartPos = Input.mousePosition;
        }
    }

}
