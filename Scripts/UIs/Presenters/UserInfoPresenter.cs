using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class UserInfoPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private UserInfoView userInfoView;
    //Model
    ViewState viewState;
    UserDataModel userDataModel;
    ActionMasterData actionMasterData;

    [Inject]
    public void Init(ViewState viewState,UserDataModel userDataModel,ActionMasterData actionMasterData)
    {
        this.viewState = viewState;
        this.userDataModel = userDataModel;
        this.actionMasterData = actionMasterData;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        //戻るボタン
        userInfoView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);

        userDataModel.playerStatus
            .Subscribe(value =>
            {
                userInfoView.SetStatus(value);
            })
            .AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.UserInfoView)
                {
                    userInfoView.Show();
                    var value = userDataModel.GetPartyParam();
                    if (value.buki != null)
                        userInfoView.SetWeapon(value.buki, actionMasterData.GetAction(value.buki.actionType1), actionMasterData.GetAction(value.buki.actionType2));
                }
                else
                {
                    userInfoView.Hide();
                }
            })
            .AddTo(gameObject);

        userDataModel.level
            .Subscribe(value => userInfoView.SetLevel(value))
            .AddTo(this);

        userDataModel.exp
           .Subscribe(value => userInfoView.SetExp(value,userDataModel.NeedExp(userDataModel.level.Value)))
           .AddTo(this);

        userDataModel.totalKcal
          .Subscribe(value => userInfoView.SetTotalKcal(value))
          .AddTo(this);

        userDataModel.distanceTraveled
          .Subscribe(value => userInfoView.SetTotalDistance(value))
          .AddTo(this);


    }
}
