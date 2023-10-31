using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Cinemachine;

/// <summary>
/// 3Dカメラの視点変更のためのクラス
/// </summary>
public class MapCamera3dManager : MonoBehaviour
{
    [SerializeField] private Transform horizontal;
    [SerializeField] private Transform vertical;
    [SerializeField] private Transform player;

    [SerializeField] private float angle = 60f;
    [SerializeField] private Camera map3dCamera;
    // スワイプの開始地点
    private Vector2 swipeStartPos;
    private ViewState viewState;
    //virtualカメラ
    [SerializeField] CinemachineVirtualCameraBase camera3d;
    [SerializeField] CinemachineVirtualCameraBase camera2d;

    [Inject]
    public void Init(ViewState viewState)
    {
        this.viewState = viewState;
    }

    [SerializeField] GameObject cine;

    private void Start()
    {
        Debug.Log(cine.transform.eulerAngles.x);
    }
    private void Update()
    {
        float targetYRotation = horizontal.eulerAngles.y;
        Vector3 newRotation = cine.transform.eulerAngles;
        newRotation.y = targetYRotation;
        cine.transform.eulerAngles = newRotation;
        if (viewState.currentView.Value == ViewState.View.Map3dView)
        {
            RotateCamera();
        }
    }

    public void ChangePriority()
    {
        camera3d.Priority = 1 - camera3d.Priority;
        camera2d.Priority = 1 - camera2d.Priority;
    }

    private void RotateCamera()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal.RotateAround(player.position, Vector3.up, angle * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal.RotateAround(player.position, Vector3.up, -angle * Time.deltaTime);
        }
    }
}


