using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3(0,10,-8);
    [SerializeField] private Transform player;
    private Vector2 swipeStartPos;
    [SerializeField] private float angle = 5f;
    [SerializeField] private Camera map3dCamera;
    private float minView = 60;
    private float maxView = 160;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.position + offset;
        if (Input.touchCount == 1)
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
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.localEulerAngles.x < 90)
            {
                //RotateAround2(player,Vector3.right,50);
                transform.RotateAround(player.position, Vector3.right, 1);
                map3dCamera.fieldOfView = Mathf.Clamp(map3dCamera.fieldOfView + 5, minView, maxView);
            }

        }

        // Bキーを押すとminに近づく
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.localEulerAngles.x >= 0)
            {
                //RotateAround2(player, -Vector3.right, 50);
                transform.RotateAround(player.position, -Vector3.right, 1);
                map3dCamera.fieldOfView = Mathf.Clamp(map3dCamera.fieldOfView - 5, minView, maxView);
            }
        }
    }
}
