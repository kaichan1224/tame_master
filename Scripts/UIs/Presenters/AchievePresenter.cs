using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class AchievePresenter : MonoBehaviour
{
    //UI
    [SerializeField] private AchieveView achieveView;
    //Model
    ViewState viewState;
    AchieveModel achieveModel;
    UserDataModel userDataModel;

    [Inject]
    public void Init(ViewState viewState,AchieveModel achieveModel,UserDataModel userDataModel)
    {
        this.viewState = viewState;
        this.achieveModel = achieveModel;
        this.userDataModel = userDataModel;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        achieveView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.AchieveView)
                {
                    achieveModel.SetAchieve();
                    achieveModel.SetLatestAchieveUpdateDay();
                    achieveView.Show();
                    achieveView.ShowAchieves(userDataModel.GetOwnedAchieves(),userDataModel.GetOwnedAchieveSprites());
                }
                else
                {
                    achieveView.Hide();
                    achieveView.HideAchieves();
                }
            })
            .AddTo(gameObject);
    }
}
