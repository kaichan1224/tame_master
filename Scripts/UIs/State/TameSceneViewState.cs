using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TameSceneViewState : MonoBehaviour
{
    public enum View
    {
        TameView,
        ClearView,
        FailView,
    };

    ReactiveProperty<View> currentViewInstance = new(View.TameView);
    public IReadOnlyReactiveProperty<View> currentView => currentViewInstance;

    public void ChangeView(View nextView)
    {
        currentViewInstance.Value = nextView;
    }
}
