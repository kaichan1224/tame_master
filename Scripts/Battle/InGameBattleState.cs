using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InGameBattleState
{
    public enum InGameBattle
    {
        Ready,//試合前
        AttackSelect,//技選択
        Attack,//攻撃
        Result,//結果表示
    }

    ReactiveProperty<InGameBattle> _currentState = new(InGameBattle.Ready);
    public IReadOnlyReactiveProperty<InGameBattle> currentState => _currentState;

    public void ChangeView(InGameBattle nextState)
    {
        _currentState.Value = nextState;
    }
}
