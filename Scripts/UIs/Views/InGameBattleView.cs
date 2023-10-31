using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UniRx;

public class InGameBattleView : MonoBehaviour
{
    //常時表示
    [SerializeField] private Slider playerHpSlider;
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private Slider enemyHpSlider;
    [SerializeField] private TMP_Text enemyHpText;
    public ActionSelectPanel actionSelectPanel;
    [SerializeField] private TMP_Text turnText;
    //3Dオブジェクト
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] Transform player3;
    [SerializeField] Transform enemy1;
    [SerializeField] Transform enemy2;
    [SerializeField] Transform enemy3;
    //スタートパネル
    [SerializeField] private GameObject startPanel;
    [SerializeField] private RectTransform startText;
    //勝利パネル
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button winExitButton;
    public IObservable<Unit> onWinExit => winExitButton.OnClickAsObservable();
    //敗北パネル
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button loseExitButton;
    public IObservable<Unit> onLoseExit => loseExitButton.OnClickAsObservable();
    //コマンド
    [SerializeField] private TMP_Text commandText;
    [SerializeField] private TMP_Text targetText;
    //プロパティ
    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    public void SetTurn(int turn)
    {
        turnText.text = turn.ToString() + "ターン";
    }

    public void SetPlayerHp(int currentHp,int maxHp)
    {
        //playerHpSlider.value = (float)currentHp / (float)maxHp;
        playerHpSlider.DOValue((float)currentHp / (float)maxHp, 1f);
        playerHpText.text = $"プレイヤーの総HP {Mathf.Max(0,currentHp)}/{maxHp}";
    }

    public void SetEnemyHp(int currentHp,int maxHp)
    {
        //enemyHpSlider.value = (float)currentHp / (float)maxHp;
        enemyHpSlider.DOValue((float)currentHp / (float)maxHp, 1f);
        enemyHpText.text = $"敵の総HP {Mathf.Max(0, currentHp)}/{maxHp}";
    }

    public void ActivateSelectPanel()
    {
        actionSelectPanel.Show();
    }

    public void ActivateStartPanel(Action endFunc)
    {
        Sequence sequence = DOTween.Sequence();
        startText.anchoredPosition = new Vector2(-900,0);
        sequence
            .AppendInterval(0.75f)
            .AppendCallback(() => startPanel.SetActive(true))
            .AppendInterval(0.1f)
            .Append(startText.DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.InOutQuart))
            .AppendInterval(0.1f)
            .Append(startText.DOAnchorPos(new Vector2(900, 0), 1f).SetEase(Ease.InOutQuart))
            .OnComplete(() =>
            {
                startPanel.SetActive(false);
                endFunc?.Invoke();
                Debug.Log("Move?");
            }); // UIアニメーション完了後に呼び出すメソッドを指定
        sequence.Play();

    }

    public void InitSelectPanel(List<BattleParam> players,List<Sprite> playerSprites)
    {
        Debug.Log("Init");
        Debug.Log(actionSelectPanel);
        actionSelectPanel.Init(players,playerSprites);
    }

    public void InactivateSelectPanel()
    {
        actionSelectPanel.Hide();
    }

    public void SetPlayer1Object(GameObject gameObject)
    {
        var v = Instantiate(gameObject,player1);
        players.Add(v);
    }

    public void SetTargetText(BattleParam targetParam,int damage)
    {
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() =>
            {
                targetText.gameObject.SetActive(true);
                if (targetParam.playerType == BattleParam.PlayerType.Player && targetParam.order == 1)
                    targetText.text = $"プレイヤーに{damage}ダメージ!";
                else
                    targetText.text = $"{targetParam.animalParam.name}に{damage}ダメージ!";
            })
            .AppendInterval(1.2f)
            .AppendCallback(() => targetText.gameObject.SetActive(false));
        sequence.Play();
    }

    /// <summary>
    /// 誰に何の攻撃をしたかを表示する
    /// </summary>
    public void SetCommandText(BattleParam battleParam)
    {
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() =>
            {
                commandText.gameObject.SetActive(true);
                if (battleParam.playerType == BattleParam.PlayerType.Player)
                {
                    if(battleParam.order == 1)
                        commandText.text = $"味方チームの攻撃\nプレイヤーの{battleParam.selectActionType.ToString()}";
                    else
                        commandText.text = $"味方チームの攻撃\n{battleParam.animalParam.name}の{battleParam.selectActionType.ToString()}";
                }
                else
                {
                    commandText.text = $"敵チームの攻撃\n{battleParam.animalParam.name}の{battleParam.selectActionType.ToString()}";
                }
            })
            .AppendInterval(1.5f)
            .AppendCallback(()=>commandText.gameObject.SetActive(false));
        sequence.Play();
    }

    //攻撃演出をするキャラを指定する
    public void SetAttackChara(BattleParam battleParam)
    {
        Debug.Log(battleParam.order);
        if (battleParam.playerType == BattleParam.PlayerType.Player)
        {
            AttackPlayer(battleParam.order);
        }
        else
        {
            AttackEnemy(battleParam.order);
        }
    }

    public void AttackPlayer(int index)
    {
        SoundManager.instance.PlaySE(SoundName.攻撃);
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(players[index].transform.DOMove(players[index].transform.position + transform.forward * 4, 0.2f).SetEase(Ease.Linear))
            .AppendInterval(0.1f)
            .Append(players[index].transform.DOMove(players[index].transform.position, 0.2f).SetEase(Ease.Linear));
        sequence.Play();
    }

    public void AttackEnemy(int index)
    {
        SoundManager.instance.PlaySE(SoundName.攻撃);
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(enemies[index].transform.DOMove(enemies[index].transform.position - transform.forward * 4, 0.2f).SetEase(Ease.Linear))
            .AppendInterval(0.1f)
            .Append(enemies[index].transform.DOMove(enemies[index].transform.position, 0.2f).SetEase(Ease.Linear));
        sequence.Play();
    }

    public void SetPlayer2Object(GameObject gameObject)
    {
        var v = Instantiate(gameObject, player2);
        players.Add(v);
    }

    public void SetPlayer3Object(GameObject gameObject)
    {
        var v = Instantiate(gameObject, player3);
        players.Add(v);
    }

    public void SetEnemy1Object(GameObject gameObject)
    {
        var v = Instantiate(gameObject, enemy1);
        enemies.Add(v);
    }

    public void SetEnemy2Object(GameObject gameObject)
    {
        var v = Instantiate(gameObject, enemy2);
        enemies.Add(v);
    }

    public void SetEnemy3Object(GameObject gameObject)
    {
        var v = Instantiate(gameObject, enemy3);
        enemies.Add(v);
    }

    public IEnumerator SetActiveWinPanelAsync(bool flag)
    {
        yield return new WaitForSeconds(3f);
        winPanel.SetActive(flag);
    }

    public IEnumerator SetActiveLosePanelAsync(bool flag)
    {
        yield return new WaitForSeconds(3f);
        losePanel.SetActive(flag);
    }
}
