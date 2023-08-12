using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using TMPro;

/// <summary>
/// キャラクターの進化時演出のモジュール
/// </summary>
public class evolution : MonoBehaviour
{
    [SerializeField] private PlayerMoveManager playerMoveManager;
    [SerializeField] private GameObject player;
    //vsのテキスト
    [SerializeField] private TMP_Text text;
    //文字が拡大する時間
    [SerializeField] private float scaleDuration = 1.0f;
    //拡大するサイズ
    [SerializeField] private float maxScale = 1.5f;
    //進化時のエフェクト
    [SerializeField] private GameObject effect;
    //音源管理クラス
    [SerializeField] private MusicManager musicManager;
    //岩を破壊する効果音
    [SerializeField] private GameObject iwaSound;
    //ユーザデータの管理クラス
    [SerializeField] private UserDataManager userDataManager;
    //背景の壁のオブジェクト
    [SerializeField] private GameObject backWall;
    //進化時の床のオブジェクト
    [SerializeField] private GameObject evoPlace;
    //壁を破壊するためののオブジェクト1
    [SerializeField] private GameObject evoBallNormal;
    //壁を破壊するためののオブジェクト2
    [SerializeField] private GameObject evoBallMuki;
    //進化時の視点カメラ
    [SerializeField] private GameObject evoCam;
    //進化時の壁1
    [SerializeField] private GameObject evoWallNormal;
    //進化時の壁2
    [SerializeField] private GameObject evoWallMuki;
    //3dマップ時のカメラ
    [SerializeField] private GameObject map3dCamera;
    //3dマップのUI
    [SerializeField] private GameObject map3dPage;
    //プレイヤーの胴体の本体
    [SerializeField] private GameObject evoBody;
    //通常の胴体
    [SerializeField] private GameObject evoNormal;
    //進化時の胴体
    [SerializeField] private GameObject evoMukimuki;
    //進化時のUI
    [SerializeField] private GameObject evoPanel;
    //内部数値
    private Vector3 ballForce = new Vector3(0,0,-1);
    private float power = 2000f;
    private int normalLevel = 10;
    private int mukimukiLevel = 20;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        userDataManager.level
        .Where(value => value == normalLevel)
        .Subscribe(_ =>
        {
            if (!userDataManager.isNormal.Value)
                StartEvoNormal();
            userDataManager.isNormal.Value = true;
        })
        .AddTo(this);

        userDataManager.level
        .Where(value => value == mukimukiLevel)
        .Subscribe(_ =>
        {
            if (!userDataManager.isMukimuki.Value)
                StartEvoMuki();
            userDataManager.isMukimuki.Value = true;
        })
        .AddTo(this);
    }

    /// <summary>
    /// Normalへの進化処理開始
    /// </summary>
    void StartEvoNormal()
    {
        playerMoveManager.downButtonDownFlag = false;
        playerMoveManager.upButtonDownFlag = false;
        playerMoveManager.rightButtonDownFlag = false;
        playerMoveManager.leftButtonDownFlag = false;
        player.SetActive(false);
        map3dPage.SetActive(false);
        // テキストの初期スケールを設定
        text.transform.localScale = Vector3.zero;
        musicManager.StopBgm();
        map3dCamera.SetActive(false);
        backWall.SetActive(true);
        evoPlace.SetActive(true);
        evoCam.SetActive(true);
        evoWallNormal.SetActive(true);
        evoBody.SetActive(true);
        evoPanel.SetActive(true);
        if (userDataManager.level.Value == normalLevel)
            evoNormal.SetActive(true);
        else
            evoMukimuki.SetActive(true);
        evoBallNormal.SetActive(true);
        map3dPage.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                evoBallNormal.GetComponent<Rigidbody>().AddForce(power * ballForce);
                Instantiate(iwaSound);
            })
            .AppendInterval(0.3f)
            .AppendCallback(()=>
            {
                Instantiate(effect, evoBody.transform.position, Quaternion.identity);
                musicManager.StartEvoBgm();
            })
            .AppendInterval(0.7f)
            .Append(text.transform.DOScale(maxScale, scaleDuration).SetEase(Ease.OutElastic))
            .AppendInterval(3.5f)
            .OnComplete(() =>
            {
                musicManager.StartIdleBgm();
                FinishEvoNormal();
            });
    }

    /// <summary>
    /// Normalの進化終了
    /// </summary>
    void FinishEvoNormal()
    {
        backWall.SetActive(false);
        map3dCamera.SetActive(true);
        evoPlace.SetActive(false);
        evoCam.SetActive(false);
        evoWallNormal.SetActive(false);
        evoBody.SetActive(false);
        evoPanel.SetActive(false);
        if (userDataManager.level.Value == normalLevel)
            evoNormal.SetActive(false);
        else
            evoMukimuki.SetActive(false);
        evoBallNormal.SetActive(false);
        map3dPage.SetActive(true);
        player.SetActive(true);
    }

    /// <summary>
    /// MukiMukiへの進化処理開始
    /// </summary>
    void StartEvoMuki()
    {
        playerMoveManager.downButtonDownFlag = false;
        playerMoveManager.upButtonDownFlag = false;
        playerMoveManager.rightButtonDownFlag = false;
        playerMoveManager.leftButtonDownFlag = false;
        map3dPage.SetActive(false);
        player.SetActive(false);
        // テキストの初期スケールを設定
        text.transform.localScale = Vector3.zero;
        musicManager.StopBgm();
        backWall.SetActive(true);
        map3dCamera.SetActive(false);
        evoPlace.SetActive(true);
        evoCam.SetActive(true);
        evoWallMuki.SetActive(true);
        evoBody.SetActive(true);
        evoPanel.SetActive(true);
        if (userDataManager.level.Value == normalLevel)
            evoNormal.SetActive(true);
        else
            evoMukimuki.SetActive(true);
        evoBallMuki.SetActive(true);
        map3dPage.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                evoBallMuki.GetComponent<Rigidbody>().AddForce(power * ballForce);
                Instantiate(iwaSound);
            })
            .AppendInterval(0.3f)
            .AppendCallback(() =>
            {
                Instantiate(effect, evoBody.transform.position, Quaternion.identity);
                musicManager.StartEvoBgm();
            })
            .AppendInterval(0.7f)
            .Append(text.transform.DOScale(maxScale, scaleDuration).SetEase(Ease.OutElastic))
            .AppendInterval(3.5f)
            .OnComplete(() =>
            {
                musicManager.StartIdleBgm();
                FinishEvoMuki();
            });
    }

    /// <summary>
    /// MukiMukiの進化終了
    /// </summary>
    void FinishEvoMuki()
    {
        backWall.SetActive(false);
        map3dCamera.SetActive(true);
        evoPlace.SetActive(false);
        evoCam.SetActive(false);
        evoWallMuki.SetActive(false);
        evoBody.SetActive(false);
        evoPanel.SetActive(false);
        if (userDataManager.level.Value == normalLevel)
            evoNormal.SetActive(false);
        else
            evoMukimuki.SetActive(false);
        evoBallMuki.SetActive(false);
        map3dPage.SetActive(true);
        player.SetActive(true);
    }
}
