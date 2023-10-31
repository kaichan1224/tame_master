using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// どのView(画面)が今表示しているかの状態をもつクラス
/// </summary>
public class ViewState
{
    public enum View
    {
        Map3dView,
        UserInfoView,
        AchieveView,
        SettingView,
        CollectionView,
        PartyView,
        BattleView,
        GachaView,
        TameStartView,
    };

    ReactiveProperty<View> currentViewInstance = new(View.Map3dView);
    public IReadOnlyReactiveProperty<View> currentView => currentViewInstance;

    public void ChangeView(View nextView)
    {
        currentViewInstance.Value = nextView;
    }
}
