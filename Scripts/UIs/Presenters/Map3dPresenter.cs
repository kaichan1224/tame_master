using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class Map3dPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private Map3dView map3DView;
    //Model
    ViewState viewState;
    UserDataModel userDataModel;
    SaveDataRepository saveDataRepository;
    TameModel tameModel;
    MapCamera3dManager cameraManager;
    [SerializeField] GameObject player;

    [Inject]
    public void Init(ViewState viewState,UserDataModel userDataModel,SaveDataRepository saveDataRepository,TameModel tameModel,MapCamera3dManager cameraManager)
    {
        this.viewState = viewState;
        this.userDataModel = userDataModel;
        this.saveDataRepository = saveDataRepository;
        this.tameModel = tameModel;
        this.cameraManager = cameraManager;
    }
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundName.通常時bgm);
        Bind();
    }

    void Bind()
    {
        tameModel.InitPlayer(player);

        map3DView.OnViewChange
            .Subscribe(_ =>
            {
                cameraManager.ChangePriority();
            })
            .AddTo(this);

        userDataModel.level
            .Subscribe(level => map3DView.SetLevel(level))
            .AddTo(this);

        userDataModel.exp
            .Subscribe(exp => map3DView.SetExp(exp,userDataModel.NeedExp(userDataModel.level.Value)))
            .AddTo(this);

        tameModel.tameAnimal
            .Skip(1)
            .Subscribe(_ => viewState.ChangeView(ViewState.View.TameStartView))
            .AddTo(this);

        //ボタンを推した時の画面遷移
        map3DView.OnAchieve
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.AchieveView);
            })
            .AddTo(this);

        map3DView.OnCollection
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.CollectionView);
            })
            .AddTo(this);


        map3DView.OnUserInfo
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.UserInfoView);
            })
            .AddTo(this);

        map3DView.OnParty
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.PartyView);
            })
            .AddTo(this);

        map3DView.OnSetting
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.SettingView);
            })
            .AddTo(this);

        map3DView.OnBattle
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.BattleView);
            })
            .AddTo(this);

        map3DView.OnGacha
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.GachaView);
            })
            .AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.Map3dView)
                {
                    map3DView.Show();
                }
                else
                {
                    map3DView.Hide();
                }
            })
            .AddTo(gameObject);
    }
}
