using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using UnityEngine.SceneManagement;

/// <summary>
/// バトル画面InGameのプレゼンタークラス
/// バグの原因
///     プレイヤーはplayersの2番目(1index)なので画像取得の際は注意!
/// </summary>
public class InGameBattlePresenter : MonoBehaviour
{
    [SerializeField] private InGameBattleView inGameBattleView;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private GameObject playerPrefab;
    private AttackModel attackModel;
    private AchieveModel achieveModel;
    private InGameBattleState inGameBattleState;
    private UserDataModel userDataModel;
    private AnimalMasterData animalMasterData;
    //Debug
    public List<BattleParam> player;
    public List<BattleParam> enemy;

    [Inject]
    public void Init(AttackModel attackModel,InGameBattleState inGameBattleState,UserDataModel userDataModel,AnimalMasterData animalMasterData,AchieveModel achieveModel)
    {
        this.attackModel = attackModel;
        this.inGameBattleState = inGameBattleState;
        this.userDataModel = userDataModel;
        this.animalMasterData = animalMasterData;
        this.achieveModel = achieveModel;
    }

    public void Start()
    {
        SoundManager.instance.PlayBGM(SoundName.バトルbgm);
        Bind();
        inGameBattleView.InitSelectPanel(attackModel.GetPlayers(), GetPlayerSprites(attackModel.GetPlayers()));
    }

    /// <summary>
    /// プレイヤーの3dモデル表示
    /// </summary>
    public void ShowCharators()
    {
        var enemies = attackModel.GetEnemies();
        inGameBattleView.SetEnemy1Object(animalMasterData.GetAnimal(enemies[0].animalParam.name).gameObject);
        inGameBattleView.SetEnemy2Object(animalMasterData.GetAnimal(enemies[1].animalParam.name).gameObject);
        inGameBattleView.SetEnemy3Object(animalMasterData.GetAnimal(enemies[2].animalParam.name).gameObject);
        inGameBattleView.SetPlayer1Object(animalMasterData.GetAnimal(userDataModel.GetPartyParam().animal1.name).gameObject);
        inGameBattleView.SetPlayer2Object(playerPrefab);
        inGameBattleView.SetPlayer3Object(animalMasterData.GetAnimal(userDataModel.GetPartyParam().animal2.name).gameObject);
    }

    /// <summary>
    /// パーティーからプレイヤー側のデータを取得して返す
    /// </summary>
    /// <returns></returns>
    public List<BattleParam> GetPlayersBattleParam()
    {
        List<BattleParam> battleParams = new List<BattleParam>();
        battleParams.Add(new BattleParam(userDataModel.GetPartyParam().animal1, BattleParam.PlayerType.Player,0));
        battleParams.Add(new BattleParam(userDataModel.GetPartyParam(), userDataModel.playerStatus.Value, BattleParam.PlayerType.Player,1));
        battleParams.Add(new BattleParam(userDataModel.GetPartyParam().animal2, BattleParam.PlayerType.Player,2));
        return battleParams;
    }

    public void Bind()
    {

        inGameBattleState.currentState
            .Where(state => state == InGameBattleState.InGameBattle.Ready)
            .Subscribe(_ =>
            {
                inGameBattleView.InactivateSelectPanel();
                attackModel.Init(GetPlayersBattleParam(),attackModel.GetSelectedEnemies());
                attackModel.InitHp();
                ShowCharators();
                inGameBattleView.InitSelectPanel(attackModel.GetPlayers(), GetPlayerSprites(attackModel.GetPlayers()));//選択画面の表示更新
                inGameBattleView.ActivateStartPanel(()=> inGameBattleState.ChangeView(InGameBattleState.InGameBattle.AttackSelect));
            })
            .AddTo(this);

        inGameBattleState.currentState
            .Where(state => state == InGameBattleState.InGameBattle.Attack)
            .Subscribe(_ =>
            {
                StartCoroutine(attackModel.AttackAllCharactor(()=> inGameBattleState.ChangeView(InGameBattleState.InGameBattle.AttackSelect)));
                inGameBattleView.InactivateSelectPanel();
            })
            .AddTo(this);

        inGameBattleState.currentState
            .Where(state => state == InGameBattleState.InGameBattle.AttackSelect)
            .Subscribe(_ =>
            {
                attackModel.SetAllPlayerNotSelected();
                inGameBattleView.ActivateSelectPanel();
            })
            .AddTo(this);

        inGameBattleState.currentState
            .Where(state => state == InGameBattleState.InGameBattle.Result)
            .Subscribe(_ =>
            {
                if (attackModel.playerTotalHp.Value <= 0)
                {
                    SoundManager.instance.PlayBGM(SoundName.失敗);
                    StartCoroutine(inGameBattleView.SetActiveLosePanelAsync(true));
                }
                if (attackModel.enemyTotalHp.Value <= 0)
                {
                    SoundManager.instance.PlayBGM(SoundName.成功);
                    StartCoroutine(inGameBattleView.SetActiveWinPanelAsync(true));
                }
            })
            .AddTo(this);

        //攻撃コマンド表示
        attackModel.attackChara
            .Skip(1)
            .Subscribe(value =>
            {
                inGameBattleView.SetCommandText(value);
                inGameBattleView.SetAttackChara(value);
            })
            .AddTo(this);

        attackModel.targetChara
            .Skip(1)
            .Subscribe(value =>
            {
                inGameBattleView.SetTargetText(value,attackModel.Damage);
            })
            .AddTo(this);


        attackModel.isStartAttack
             .Subscribe(flag =>
             {
                 if (flag)
                 {
                     //Debug
                     player = attackModel.GetPlayers();
                     enemy = attackModel.GetEnemies();
                     inGameBattleState.ChangeView(InGameBattleState.InGameBattle.Attack);
                 }
             })
             .AddTo(this);

        attackModel.playerTotalHp
            .Subscribe(value =>
            {
                inGameBattleView.SetPlayerHp(value, attackModel.maxPlayerTotalHp);
            }).AddTo(this);

        attackModel.enemyTotalHp
            .Subscribe(value =>
            {
                inGameBattleView.SetEnemyHp(value,attackModel.maxEnemyTotalHp);
            }).AddTo(this);

        attackModel.turn
            .Subscribe(value =>
            {
                inGameBattleView.SetTurn(value);
            }).AddTo(this);

        inGameBattleView.actionSelectPanel.Onplayer1A
            .Subscribe(_ =>
            {
                SoundManager.instance.PlaySE(SoundName.選択);
                attackModel.SetPlayerActionType(0,1);
            })
            .AddTo(this);

        inGameBattleView.actionSelectPanel.Onplayer1B
            .Subscribe(_ =>
            {
                SoundManager.instance.PlaySE(SoundName.選択);
                attackModel.SetPlayerActionType(0, 2);
            })
            .AddTo(this);

        inGameBattleView.actionSelectPanel.Onplayer2A
            .Subscribe(_ =>
            {
                SoundManager.instance.PlaySE(SoundName.選択);
                attackModel.SetPlayerActionType(1, 1);
            })
            .AddTo(this);

        inGameBattleView.actionSelectPanel.Onplayer2B
            .Subscribe(_ =>
            {
                SoundManager.instance.PlaySE(SoundName.選択);
                attackModel.SetPlayerActionType(1, 2);
            })
            .AddTo(this);

        inGameBattleView.actionSelectPanel.Onplayer3A
            .Subscribe(_ =>
            {
                SoundManager.instance.PlaySE(SoundName.選択);
                attackModel.SetPlayerActionType(2, 1);
            })
            .AddTo(this);

        inGameBattleView.actionSelectPanel.Onplayer3B
            .Subscribe(_ =>
            {
                SoundManager.instance.PlaySE(SoundName.選択);
                attackModel.SetPlayerActionType(2, 2);
            })
            .AddTo(this);

        inGameBattleView.onLoseExit
            .Subscribe(_ =>
            { 
                SceneManager.LoadScene("MainScene");
            }).AddTo(this);

        inGameBattleView.onWinExit
            .Subscribe(_ =>
            {
                achieveModel.UpdateBattleWinAchieve();
                userDataModel.IncrementGachaTicket(1);
                SceneManager.LoadScene("MainScene");
            }).AddTo(this);



    }

    /// <summary>
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private List<Sprite> GetPlayerSprites(List<BattleParam> player)
    {
        List<Sprite> playerSprites = new List<Sprite>();
        playerSprites.Add(animalMasterData.GetSprite(player[0].animalParam.name));
        playerSprites.Add(playerSprite);
        playerSprites.Add(animalMasterData.GetSprite(player[2].animalParam.name));
        Debug.Log(playerSprites);
        return playerSprites;
    }
}
