using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using VContainer;
using Cysharp.Threading.Tasks;
using System;
/// <summary>
/// 攻撃、体力管理等を行うバトルに関するメインクラス
/// </summary>
public class AttackModel
{
    //model
    private ActionMasterData actionMasterData;
    private AnimalParamMasterData animalParamMasterData;
    private InGameBattleState inGameBattleState;
    private UserDataModel userDataModel;

    //パラメータ
    private List<BattleParam> players;
    private List<BattleParam> enemies;
    public ReactiveProperty<int> playerTotalHp = new();
    public ReactiveProperty<int> enemyTotalHp = new();
    public ReactiveProperty<int> turn = new();
    private int _maxPlayerTotalHp;
    public int maxPlayerTotalHp => _maxPlayerTotalHp;
    private int _maxEnemyTotalHp;
    public int maxEnemyTotalHp => _maxEnemyTotalHp;
    private bool isPlayer1Selected;
    private bool isPlayer2Selected;
    private bool isPlayer3Selected;
    public ReactiveProperty<bool> isStartAttack = new();
    //通知用
    public ReactiveProperty<BattleParam> attackChara = new();
    public ReactiveProperty<BattleParam> targetChara = new();
    private int damage;
    public int Damage => damage;

    [Inject]
    public AttackModel(ActionMasterData actionMasterData, AnimalParamMasterData animalParamMasterData,InGameBattleState inGameBattleState,UserDataModel userDataModel)
    {
        this.actionMasterData = actionMasterData;
        this.animalParamMasterData = animalParamMasterData;
        this.inGameBattleState = inGameBattleState;
        this.userDataModel = userDataModel;
    }

    public List<BattleParam> GetEnemies()
    {
        return enemies;
    }

    public List<BattleParam> GetPlayers()
    {
        return players;
    }

    public void SetAllPlayerNotSelected()
    {
        isStartAttack.Value = false;
        isPlayer1Selected = false;
        isPlayer2Selected = false;
        isPlayer3Selected = false;
    }

    public void SetIsSelected(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                isPlayer1Selected = true;
                break;
            case 1:
                isPlayer2Selected = true;
                break;
            case 2:
                isPlayer3Selected = true;
                break;
        }
        if (isPlayer1Selected && isPlayer2Selected && isPlayer3Selected)
            isStartAttack.Value = true;
    }

    /// <summary>
    /// ランダムに敵3体生成する(全員動物)
    /// </summary>
    /// <returns></returns>
    public List<BattleParam> GetSelectedEnemies()
    {
        List<BattleParam> enemies = new List<BattleParam>();
        switch (userDataModel.battleType)
        {
            case BattleType.Easy:
                var animalParam0 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.兎);
                enemies.Add(new BattleParam(animalParam0, BattleParam.PlayerType.Enemy, 0));
                var animalParam1 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.羊);
                enemies.Add(new BattleParam(animalParam1, BattleParam.PlayerType.Enemy, 1));
                var animalParam2 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.蛇);
                enemies.Add(new BattleParam(animalParam2, BattleParam.PlayerType.Enemy, 2));
                break;
            case BattleType.Normal:
                var animalParam3 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.白馬);
                enemies.Add(new BattleParam(animalParam3, BattleParam.PlayerType.Enemy, 0));
                var animalParam4 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.狼);
                enemies.Add(new BattleParam(animalParam4, BattleParam.PlayerType.Enemy, 1));
                var animalParam5 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.タイガー);
                enemies.Add(new BattleParam(animalParam5, BattleParam.PlayerType.Enemy, 2));
                break;
            case BattleType.Hard:
                var animalParam6 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.ゴリラ);
                enemies.Add(new BattleParam(animalParam6, BattleParam.PlayerType.Enemy, 0));
                var animalParam7 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.ライオン);
                enemies.Add(new BattleParam(animalParam7, BattleParam.PlayerType.Enemy, 1));
                var animalParam8 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.青狼);
                enemies.Add(new BattleParam(animalParam8, BattleParam.PlayerType.Enemy, 2));
                break;
            case BattleType.VeryHard:
                var animalParam9 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.アパトサウルス);
                enemies.Add(new BattleParam(animalParam9, BattleParam.PlayerType.Enemy, 0));
                var animalParam10 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.ヴェラキラプトル);
                enemies.Add(new BattleParam(animalParam10, BattleParam.PlayerType.Enemy, 1));
                var animalParam11 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.ステゴサウルス);
                enemies.Add(new BattleParam(animalParam11, BattleParam.PlayerType.Enemy, 2));
                break;
            case BattleType.Hell:
                var animalParam12 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.ティラノサウルス);
                enemies.Add(new BattleParam(animalParam12, BattleParam.PlayerType.Enemy, 0));
                var animalParam13 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.トリケラトプス);
                enemies.Add(new BattleParam(animalParam13, BattleParam.PlayerType.Enemy, 1));
                var animalParam14 = animalParamMasterData.GetAnimalParam(AnimalParam.ANIMAL_NAME.ティラノサウルス);
                enemies.Add(new BattleParam(animalParam14, BattleParam.PlayerType.Enemy, 2));
                break;
        }
        return enemies;
    }

    public void Init(List<BattleParam> players, List<BattleParam> enemies)
    {
        this.players = players;
        this.enemies = enemies;
        turn.Value = 1;
    }
    /// <summary>
    /// プレイヤーと敵の体力を初期化する
    /// </summary>
    /// <param name="players"></param>
    /// <param name="enemies"></param>
    public void InitHp()
    {
        playerTotalHp.Value = 0;
        enemyTotalHp.Value = 0;
        foreach (var player in players)
        {
            playerTotalHp.Value += player.status.Hp;
        }

        foreach (var enemy in enemies)
        {
            enemyTotalHp.Value += enemy.status.Hp;
        }
        _maxPlayerTotalHp = playerTotalHp.Value;
        _maxEnemyTotalHp = enemyTotalHp.Value;
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="attack">基礎攻撃力</param>
    /// <param name="defense">防御力</param>
    /// <param name="skillAttack">スキル攻撃力</param>
    /// <param name="hitRate">命中率</param>
    /// <param name="criticalRate">会心率</param>
    /// <returns></returns>
    public int DamageCalculate(int attack,int defense,int skillAttack,double hitRate,double criticalRate)
    {
        //（ (スキルの攻撃力+基礎攻撃力)* 定数1 -基礎防御力 * 定数2 ）*補正(乱数調整0.9~1.1)
        // 会心率が乱数を上回ったら,1.5倍ダメージアップ
        // 命中率が乱数を下回ったら,ヒットしない
        var r1 = UnityEngine.Random.Range(0, 1f);
        var r2 = UnityEngine.Random.Range(0, 1f);
        if (r1 > hitRate)
            return 0;
        if (r2 < criticalRate)
            return Mathf.Max((int)(((attack + skillAttack) * 4 - defense * 3.5) * UnityEngine.Random.Range(0.9f, 1.1f) * 1.5f),0);
        return Mathf.Max((int)(((attack + skillAttack) * 4 - defense * 3.5) * UnityEngine.Random.Range(0.9f,1.1f)),0);
    }

    /// <summary>
    /// 回復量を取得
    /// </summary>
    /// <param name="healValue"></param>
    /// <returns></returns>
    public int Heal(int healValue)
    {
        return healValue;
    }

    /// <summary>
    /// 速度が早い順に並び替える
    /// </summary>
    public List<BattleParam> SortBySpeed(List<BattleParam> players,List<BattleParam> enemies)
    {
        List<BattleParam> allCharactors = players.Concat(enemies).ToList();
        return allCharactors.OrderBy(value => value.status.Speed).ToList();
    }

    /// <summary>
    /// プレイヤーの行動を選択する
    /// </summary>
    /// <param name="playerIndex">何番目のプレイヤーか</param>
    /// <param name="actiontypeNum">type</param>
    public void SetPlayerActionType(int playerIndex,int actionTypeNum)
    {
        switch (actionTypeNum)
        {
            case 1:
                players[playerIndex].selectActionType = players[playerIndex].actionType1;
                break;
            case 2:
                players[playerIndex].selectActionType = players[playerIndex].actionType2;
                break;
            default:
                players[playerIndex].selectActionType = ActionType.体当たり;
                break;
        }
        SetIsSelected(playerIndex);
    }

    /// <summary>
    /// キャラクター全員が選択した攻撃で自動で攻撃する
    /// </summary>
    /// <param name="allCharactors"></param>
    public IEnumerator AttackAllCharactor(Action endfunc)
    {
        //敵の行動選択
        SetEnemyAction();
        //早い順にsort
        var allCharactorsSorted = SortBySpeed(players,enemies);
        foreach (var charactor in allCharactorsSorted)
        {
            //通知用
            attackChara.Value = charactor;
            var actionParam = actionMasterData.GetAction(charactor.selectActionType);
            var statusParam = charactor.status;
            //通知する必要あり
            if (charactor.playerType == BattleParam.PlayerType.Player)
            {
                var target = GetTargetChara(GetEnemies());
                damage = DamageCalculate(statusParam.Attack, target.status.Defense, actionParam.skillAttack, actionParam.hitRate, actionParam.criticalRate);
                targetChara.Value = target;
                enemyTotalHp.Value -= damage;
                playerTotalHp.Value = Mathf.Min(playerTotalHp.Value + Heal(actionParam.healValue),_maxPlayerTotalHp);
            }
            else
            {
                var target = GetTargetChara(GetPlayers());
                damage = DamageCalculate(statusParam.Attack, target.status.Defense, actionParam.skillAttack, actionParam.hitRate, actionParam.criticalRate);
                targetChara.Value = target;
                playerTotalHp.Value -= damage;
                enemyTotalHp.Value = Mathf.Min(enemyTotalHp.Value + Heal(actionParam.healValue),_maxEnemyTotalHp);
            }
            if (enemyTotalHp.Value < 0 || playerTotalHp.Value < 0)
            {
                inGameBattleState.ChangeView(InGameBattleState.InGameBattle.Result);
                yield break;
            }
            yield return new WaitForSeconds(1.5f);
        }
        turn.Value += 1;
        endfunc?.Invoke();
    }

    public void SetEnemyAction()
    {
        var r1 = UnityEngine.Random.Range(0, 1f);
        var r2 = UnityEngine.Random.Range(0, 1f);
        var r3 = UnityEngine.Random.Range(0, 1f);
        if(r1>0.5f)
            enemies[0].selectActionType = enemies[0].actionType1;
        else
            enemies[0].selectActionType = enemies[0].actionType2;
        if (r2 > 0.5f)
            enemies[1].selectActionType = enemies[1].actionType1;
        else
            enemies[1].selectActionType = enemies[1].actionType2;
        if (r3 > 0.5f)
            enemies[2].selectActionType = enemies[2].actionType1;
        else
            enemies[2].selectActionType = enemies[2].actionType2;
    }

    public BattleParam GetTargetChara(List<BattleParam> chara)
    {
        return chara[UnityEngine.Random.Range(0, chara.Count)];
    }


    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    public void GameSet()
    {
        
    }

}
