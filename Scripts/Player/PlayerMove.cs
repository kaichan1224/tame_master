/**************************************
 *** Designer:AL21115
 *** Date:2023.5.23
 *** プレイヤーの移動
 *** Last Editor:AL21115
 *** Last Edited:2023.6.13
***************************************/
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Unity.Collections;
using DG.Tweening;

/// <summary>
/// プレイヤーが位置情報に基づいて移動する制御を行うクラス
/// TODO DataUpdateは実機の際はLonlatGetter内でデータを保存するようにするため、不要
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private AbstractMap map3d;
    [SerializeField] private LocationUpdater locationUpdater;
    [SerializeField] private PlayerAnimation playerAnimation;
    private float speed = 10f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0,0,speed * Time.deltaTime);
            playerAnimation.SetMoveAnimation(true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
