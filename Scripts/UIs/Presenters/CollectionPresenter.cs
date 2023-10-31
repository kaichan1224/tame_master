using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class CollectionPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private CollectionView collectionView;
    //Model
    ViewState viewState;
    UserDataModel userDataModel;
    AnimalMasterData animalMasterData;

    [Inject]
    public void Init(ViewState viewState,UserDataModel userDataModel,AnimalMasterData animalMasterData)
    {
        this.viewState = viewState;
        this.userDataModel = userDataModel;
        this.animalMasterData = animalMasterData;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        collectionView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);

        collectionView.AnimalPanel
            .onExit
            .Subscribe(_ =>
            {
                collectionView.HideAnimalPanel();
            }).AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.CollectionView)
                {
                    //TODO:ここで動物の表示更新を行う必要がある
                    collectionView.HideAnimals();
                    var datas = userDataModel.GetOwnedAnimals();
                    collectionView.ShowAnimals(datas,animalMasterData.GetSprites(datas));
                    collectionView.Show();
                    collectionView.HideAnimalPanel();
                    SetOnClickAnimalButton();
                }
                else
                {
                    collectionView.Hide();
                }
            })
            .AddTo(gameObject);
    }

    public void SetOnClickAnimalButton()
    {
        foreach (var itemButton in collectionView.animalButtons)
        {
            itemButton.GetButton().OnClickAsObservable()
                .Subscribe(_ =>
                {
                    collectionView.InitAnimalPanel(itemButton.GetAnimalParam());
                    collectionView.ShowAnimalPanel();
                }).AddTo(this);
        }
    }
}
