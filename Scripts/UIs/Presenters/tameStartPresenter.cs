using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using System;

public class tameStartPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private tameStartView tameStartView;
    //Model
    ViewState viewState;
    TameModel tameModel;
    AnimalMasterData animalMasterData;

    [Inject]
    public void Init(ViewState viewState,TameModel tameModel,AnimalMasterData animalMasterData)
    {
        this.viewState = viewState;
        this.tameModel = tameModel;
        this.animalMasterData = animalMasterData;
    }

    void Start()
    {
        Bind();
    }

    void Bind()
    {
        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.TameStartView)
                {
                    tameStartView.Show();
                }
                else
                {
                    tameStartView.Hide();
                }
            })
            .AddTo(gameObject);

        tameModel.tameAnimal
            .Skip(1)
            .Subscribe(animal =>
            {
                //TODOプレイヤー画像
                SoundManager.instance.PlayBGM(SoundName.テイム中bgm);
                tameStartView.StartTame(null,animalMasterData.GetSprite(animal.name),tameModel.StartTameScene);
            })
            .AddTo(this);

    }
}
