using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using UnityEngine.SceneManagement;


public class BattlePresenter : MonoBehaviour
{
    //UI
    [SerializeField] private BattleView battleView;
    //Model
    ViewState viewState;
    UserDataModel userDataModel;

    [Inject]
    public void Init(ViewState viewState,UserDataModel userDataModel)
    {
        this.viewState = viewState;
        this.userDataModel = userDataModel;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        battleView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);


        battleView.OnEasy
            .Subscribe(_ =>
            {
                userDataModel.battleType = BattleType.Easy;
                SceneManager.LoadScene("BattleScene");
            })
            .AddTo(this);

        battleView.OnNormal
            .Subscribe(_ =>
            {
                userDataModel.battleType = BattleType.Normal;
                SceneManager.LoadScene("BattleScene");
            })
            .AddTo(this);

        battleView.OnHard
            .Subscribe(_ =>
            {
                userDataModel.battleType = BattleType.Hard;
                SceneManager.LoadScene("BattleScene");
            })
            .AddTo(this);

        battleView.OnVeryHard
            .Subscribe(_ =>
            {
                userDataModel.battleType = BattleType.VeryHard;
                SceneManager.LoadScene("BattleScene");
            })
            .AddTo(this);

        battleView.OnHell
            .Subscribe(_ =>
            {
                userDataModel.battleType = BattleType.Hell;
                SceneManager.LoadScene("BattleScene");
            })
            .AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.BattleView)
                {
                    battleView.Show();
                }
                else
                {
                    battleView.Hide();
                }
            })
            .AddTo(gameObject);
    }
}
