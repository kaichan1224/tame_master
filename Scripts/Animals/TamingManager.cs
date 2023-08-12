using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 動物のテイムを開始する際の制御を行うクラス
/// </summary>
public class TamingManager : MonoBehaviour
{
    [SerializeField] private PlayerMoveManager playerMoveManager;
    [SerializeField] private Camera tameCamera;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private AudioSource attackSound;
    public AnimalSpawn animalSpawn;
    [SerializeField] private MusicManager musicManager;
    //テイムを行う時に使用するカメラ
    [SerializeField] private GameObject tamingCamera;
    //テイムする場所
    public GameObject tamingPlace;
    //3dマップ表示時のカメラ
    [SerializeField] private GameObject map3dCamera;
    //3dページの表示画面
    [SerializeField] private GameObject map3dPage;
    //テイム画面の表示画面
    [SerializeField] private GameObject tamingPage;
    [SerializeField] private OwnedAnimalDataManager ownedAnimalDataManager;

    [SerializeField] private UserDataManager userDataManager;

    [SerializeField] private TMP_Text countDownText;
    //体力ゲージとかのゲージがあるパネル
    [SerializeField] private GameObject tameStartPanel;
    [SerializeField] private GameObject tameResultPanel;
    [SerializeField] private TMP_Text resultText;
     
    //カットイン演出
    //演出用のパネルのボタン
    [SerializeField] private RectTransform vsPanel;
    [SerializeField] private RectTransform vsPanelPlayer;
    [SerializeField] private RectTransform vsPanelEnemy;
    [SerializeField] private Image vsImage;
    [SerializeField] private RectTransform vsImageRect;
    [SerializeField] private Image flashPanelImage;
    [SerializeField] private RectTransform flashPanel;
    //実際に使用するプレイヤー画像
    private Sprite playerIcon;
    [SerializeField] private Sprite garigariIcon;
    [SerializeField] private Sprite normalIcon;
    [SerializeField] private Sprite mukimukiIcon;
    [SerializeField] private Image playerImage;
    [SerializeField] private Image enemyImage;
    //スライダー
    [SerializeField] private Slider AnimalHpSlider;
    [SerializeField] private Slider PlayerHpSlider;
    [SerializeField] private Image hpPlayerIcon;
    [SerializeField] private Image hpEnemyIcon;

    [SerializeField] private bool isNowTaming = false;
    private GameObject tamingAnimal;

    private float timer = 15f;
    private float enemyTimer = 1f;
    [SerializeField] private float StartTime = 15f;
    [SerializeField] private TMP_Text timerText;
    private int initPlayerHP;
    private int initEnemyHP;
    private int currentPlayerHp;
    private int currentEnemyHp;
    private int gariRankPower = 6;
    private int normalRankPower = 11;
    private int mukiRankPower = 16;
    private int enemyPower;
    private int playerPower;
    /// <summary>
    /// テイムが始まった時の処理
    /// </summary>
    public void StartTame(GameObject tamingAnimal)
    {
        playerMoveManager.downButtonDownFlag = false;
        playerMoveManager.upButtonDownFlag = false;
        playerMoveManager.leftButtonDownFlag = false;
        playerMoveManager.rightButtonDownFlag = false;
        if (userDataManager.level.Value >= 1 && userDataManager.level.Value <= 9)
        {
            initPlayerHP = 30;
            playerPower = 1;
            playerIcon = garigariIcon;
        }
        else if (userDataManager.level.Value >= 10 && userDataManager.level.Value <= 19)
        {
            initPlayerHP = 45;
            playerPower = 5;
            playerIcon = normalIcon;
        }
        else
        {
            initPlayerHP = 100;
            playerPower = 15;
            playerIcon = mukimukiIcon;
        }
        musicManager.StartBattleBgm();
        this.tamingAnimal = tamingAnimal;
        initEnemyHP = this.tamingAnimal.GetComponent<Animal>().parameter.hp;
        Debug.Log(initEnemyHP);
        var rank = this.tamingAnimal.GetComponent<Animal>().parameter.rank;
        if (rank == AnimalData.ANIMAL_RANK.GARIGARI)
            enemyPower = gariRankPower;
        else if (rank == AnimalData.ANIMAL_RANK.NORMAL)
            enemyPower = normalRankPower;
        else
            enemyPower = mukiRankPower;
        currentEnemyHp = initEnemyHP;
        currentPlayerHp = initPlayerHP;
        AnimalHpSlider.value = 1;
        PlayerHpSlider.value = 1;
        timer = StartTime;
        tameStartPanel.SetActive(false);
        this.tamingAnimal = tamingAnimal;
        map3dPage.SetActive(false);
        enemyImage.sprite = this.tamingAnimal.GetComponent<Animal>().parameter.sprite;
        hpEnemyIcon.sprite = this.tamingAnimal.GetComponent<Animal>().parameter.sprite;
        playerImage.sprite = playerIcon;
        hpPlayerIcon.sprite = playerIcon;
        //パネルがオンになる前に諸々の初期位置に移動する
        vsPanelPlayer.anchoredPosition = new Vector2(-1722,0);
        vsPanelEnemy.anchoredPosition = new Vector2(-64,0);
        vsImageRect.localScale = new Vector3(1, 1, 1);
        vsPanel.gameObject.SetActive(true);
        flashPanel.gameObject.SetActive(true);
        // UIアニメーション処理のシーケンスを定義
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(flashPanelImage.DOFade(endValue: 1f, duration: 0.15f))
            .Append(flashPanelImage.DOFade(endValue: 0f, duration: 0.15f))
            .Append(flashPanelImage.DOFade(endValue: 1f, duration: 0.15f))
            .Append(flashPanelImage.DOFade(endValue: 0f, duration: 0.15f))
            .Append(vsPanelEnemy.DOAnchorPos(new Vector2(-834,0), 0.5f).SetEase(Ease.InOutQuart))
            .Join(vsPanelPlayer.DOAnchorPos(new Vector2(-986, 0), 0.5f).SetEase(Ease.InOutQuart))
            .AppendInterval(0.1f)
            .Append(vsImage.DOFade(endValue:1f,duration:0.3f))
            .Append(vsImageRect.DOScale(4f,1f))
            .OnComplete(OnUIAnimationComplete); // UIアニメーション完了後に呼び出すメソッドを指定
        // シーケンスの実行開始
        sequence.Play();
    }

    /// <summary>
    /// UIアニメーション完了後に呼び出すメソッド
    /// </summary>
    private void OnUIAnimationComplete()
    {
        vsPanel.gameObject.SetActive(false);
        vsImage.DOFade(endValue: 0f, duration: 0.0f);
        this.tamingAnimal.transform.position = new Vector3(0, 38.4f, -3.6f);
        this.tamingAnimal.transform.rotation = Quaternion.Euler(0, 0, 0);
        tamingPlace.SetActive(true);
        map3dCamera.SetActive(false);
        tamingCamera.SetActive(true);
        tamingPage.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() => UpdateCountDownText("3"))
            .AppendInterval(1f)
            .AppendCallback(() => UpdateCountDownText("2"))
            .AppendInterval(1f)
            .AppendCallback(() => UpdateCountDownText("1"))
            .AppendInterval(1f)
            .AppendCallback(() => UpdateCountDownText("TAP!!"))
            .OnComplete(() =>
            {
                tameStartPanel.SetActive(true);
                isNowTaming = true;
            });
        flashPanel.gameObject.SetActive(false);
    }

    private void UpdateCountDownText(string text)
    {
        countDownText.text = text;
    }

    /// <summary>
    /// テイムが成功した時の処理
    /// </summary>
    public void TameComplete()
    {
        musicManager.StartSuccessBgm();
        isNowTaming = false;
        UpdateCountDownText("");
        tameStartPanel.SetActive(false);
        tameResultPanel.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() => resultText.text = "バトル勝利!\nテイム成功")
            .AppendInterval(3f)
            .OnComplete(() =>
            {
                ownedAnimalDataManager.AddData(tamingAnimal.GetComponent<Animal>().parameter);
                userDataManager.UpdateTameInfo(tamingAnimal.GetComponent<Animal>().parameter.id);
                tameResultPanel.SetActive(false);
                tamingAnimal.Destroy();
                tamingPage.SetActive(false);
                tamingPlace.SetActive(false);
                map3dCamera.SetActive(true);
                tamingCamera.SetActive(false);
                map3dPage.SetActive(true);
                musicManager.StartIdleBgm();
                //このコードを各場所に注意
                userDataManager.UpdateExp(50);
                userDataManager.userData.getAnimalList[tamingAnimal.GetComponent<Animal>().parameter.id] += 1;
                //TODO getAnimalListの中で値が0ではないものの個数をValueにいれる
                int tmp = 0;
                foreach (var item in userDataManager.userData.getAnimalList)
                {
                    if (item != 0)
                        tmp++;
                }
                userDataManager.tameSort.Value = tmp;
            });
        animalSpawn.currentSpwanCnt--;
    }

    /// <summary>
    /// テイム失敗時の処理
    /// </summary>
    public void TameFailed()
    {
        musicManager.StartFailBgm();
        isNowTaming = false;
        UpdateCountDownText("");
        tameStartPanel.SetActive(false);
        tameResultPanel.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() => resultText.text = "敗北...\nテイム失敗")
            .AppendInterval(3f)
            .OnComplete(() =>
            {
                tameResultPanel.SetActive(false);
                tamingAnimal.Destroy();
                tamingPage.SetActive(false);
                tamingPlace.SetActive(false);
                map3dCamera.SetActive(true);
                tamingCamera.SetActive(false);
                map3dPage.SetActive(true);
                musicManager.StartIdleBgm();
            });
        animalSpawn.currentSpwanCnt--;
    }

    /// <summary>
    /// 時間ぎれの時の処理
    /// </summary>
    public void TameOver()
    {
        musicManager.StartFailBgm();
        isNowTaming = false;
        UpdateCountDownText("");
        tameStartPanel.SetActive(false);
        tameResultPanel.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() => resultText.text = "時間ぎれ...\nテイム失敗")
            .AppendInterval(3f)
            .OnComplete(() =>
            {
                tameResultPanel.SetActive(false);
                tamingAnimal.Destroy();
                tamingPage.SetActive(false);
                tamingPlace.SetActive(false);
                map3dCamera.SetActive(true);
                tamingCamera.SetActive(false);
                map3dPage.SetActive(true);
                musicManager.StartIdleBgm();
            });
        animalSpawn.currentSpwanCnt--;
    }

    /// <summary>
    /// ゲーム中の処理を行うメソッド
    /// </summary>
    void Update()
    {
        if (isNowTaming)
        {

            Taming();
        }
    }

    /// <summary>
    /// テイムゲームのメイン処理
    /// </summary>
    void Taming()
    {
        if (timer <= 0)
        {
            isNowTaming = false;
            TameOver();
        }
        else
        {
            timerText.text = timer.ToString("00");
            timer -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //一回クリックしたら体力を1減らせる
            var mousePosition = Input.mousePosition;
            mousePosition.z = 3f;
            GameObject clone = Instantiate(hitEffect, tameCamera.ScreenToWorldPoint(mousePosition),Quaternion.identity);
            Destroy(clone, 1);
            attackSound.PlayOneShot(attackSound.clip);
            currentEnemyHp -= playerPower;
        }
        enemyTimer -= Time.deltaTime;
        if (enemyTimer < 0)
        {
            currentPlayerHp -= Random.Range(4,enemyPower);
            enemyTimer = Random.Range(0.1f, 1f);
        }
        HpUpdate();
        if (currentPlayerHp <= 0)
        {
            TameFailed();
        }
        if (currentEnemyHp <= 0)
        {
            TameComplete();
        }
    }

    void HpUpdate()
    {
        AnimalHpSlider.value = (float)currentEnemyHp / (float)initEnemyHP;
        PlayerHpSlider.value = (float)currentPlayerHp / (float)initPlayerHP;
    }
}
