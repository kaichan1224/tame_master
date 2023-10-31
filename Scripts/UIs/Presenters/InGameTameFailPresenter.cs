using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class InGameTameFailPresenter : MonoBehaviour
{
    [SerializeField] private InGameTameFailView inGameTameFailView;
    private TameSceneViewState tameSceneViewState;
    private TameModel tameModel;

    [Inject]
    public void Init(TameModel tameModel, TameSceneViewState tameSceneViewState)
    {
        this.tameModel = tameModel;
        this.tameSceneViewState = tameSceneViewState;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        inGameTameFailView.OnBack
            .Subscribe( _ =>
            {
                tameModel.FinishTameScene();
            })
            .AddTo(this);
            
        // 画面の状態を監視して画面を表示・非表示
        tameSceneViewState.currentView
            .Subscribe(view =>
            {
                if (view == TameSceneViewState.View.FailView)
                {
                    SoundManager.instance.PlayBGM(SoundName.失敗);
                    inGameTameFailView.Show();
                }
                else
                {
                    inGameTameFailView.Hide();
                }
            })
            .AddTo(gameObject);
    }

}
