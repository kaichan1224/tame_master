using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class InGameTameClearPresenter : MonoBehaviour
{
    [SerializeField] private InGameTameClearView inGameTameClearView;
    private TameSceneViewState tameSceneViewState;
    private TameModel tameModel;
    private AchieveModel achieveModel;
    private UserDataModel userDataModel;

    [Inject]
    public void Init(TameModel tameModel, TameSceneViewState tameSceneViewState,AchieveModel achieveModel,UserDataModel userDataModel)
    {
        this.tameModel = tameModel;
        this.tameSceneViewState = tameSceneViewState;
        this.achieveModel = achieveModel;
        this.userDataModel = userDataModel;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        inGameTameClearView.OnBack
            .Subscribe(_ =>
            {
                tameModel.FinishTameScene();
            })
            .AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        tameSceneViewState.currentView
            .Subscribe(view =>
            {
                if (view == TameSceneViewState.View.ClearView)
                {
                    SoundManager.instance.PlayBGM(SoundName.成功);
                    inGameTameClearView.Show();
                    achieveModel.UpdateTameAnimalNumAchieve();
                }
                else
                {
                    inGameTameClearView.Hide();
                }
            })
            .AddTo(gameObject);
    }
}
