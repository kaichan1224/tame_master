using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using System.Linq;
using static MoreMountains.Tools.MMInput;

public class PartyPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private PartyView partyView;
    [SerializeField] private Sprite nullSprite;
    //Model
    ViewState viewState;
    UserDataModel userDataModel;
    ItemSpriteMasterData itemSpriteMasterData;
    AnimalMasterData animalMasterData;

    [Inject]
    public void Init(ViewState viewState,UserDataModel userDataModel,ItemSpriteMasterData itemSpriteMasterData,AnimalMasterData animalMasterData)
    {
        this.viewState = viewState;
        this.userDataModel = userDataModel;
        this.itemSpriteMasterData = itemSpriteMasterData;
        this.animalMasterData = animalMasterData;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {

        //戻るボタン
        partyView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);
        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.PartyView)
                {
                    //TODO:所有してないやつは非表示用の画像を出すようにする
                    var party = userDataModel.GetPartyParam();
                    if(party.akuse != null)
                        partyView.SetAkusesari(itemSpriteMasterData.GetSprite(party.akuse.name));
                    else
                        partyView.SetAkusesari(nullSprite);
                    if (party.buki != null)
                        partyView.SetBuki(itemSpriteMasterData.GetSprite(party.buki.name));
                    else
                        partyView.SetBuki(nullSprite);
                    if (party.bougu != null)
                        partyView.SetBougu(itemSpriteMasterData.GetSprite(party.bougu.name));
                    else
                        partyView.SetBougu(nullSprite);
                    if (party.animal1 != null)
                        partyView.SetAnimal1(animalMasterData.GetSprite(party.animal1.name));
                    else
                        partyView.SetAnimal1(nullSprite);
                    if (party.animal2 != null)
                        partyView.SetAnimal2(animalMasterData.GetSprite(party.animal2.name));
                    else
                        partyView.SetAnimal2(nullSprite);
                    partyView.HideItems();
                    //最初に武器一覧を表示させとく
                    var datas = userDataModel.GetOwnedItems(ItemParam.ItemType.武器);
                    partyView.ShowItems(datas, itemSpriteMasterData.GetSprites(datas));
                    partyView.SetText("武器一覧");
                    partyView.Show();
                    //生成してあるボタンの設定
                    SetOnClickItemButtons();
                }
                else
                {
                    partyView.Hide();
                }
            })
            .AddTo(gameObject);

        partyView.OnAkuse
            .Subscribe(_ =>
            {
                partyView.HideItems();
                var datas = userDataModel.GetOwnedItems(ItemParam.ItemType.アクセサリー);
                partyView.ShowItems(datas,itemSpriteMasterData.GetSprites(datas));
                partyView.SetText("アクセサリ一覧");
                SetOnClickItemButtons();
            }).AddTo(this);

        partyView.OnBougu
            .Subscribe(_ =>
            {
                partyView.HideItems();
                var datas = userDataModel.GetOwnedItems(ItemParam.ItemType.防具);
                partyView.ShowItems(datas, itemSpriteMasterData.GetSprites(datas));
                partyView.SetText("防具一覧");
                SetOnClickItemButtons();
            }).AddTo(this);

        partyView.OnBuki
            .Subscribe(_ =>
            {
               
                partyView.HideItems();
                var datas = userDataModel.GetOwnedItems(ItemParam.ItemType.武器);
                partyView.ShowItems(datas, itemSpriteMasterData.GetSprites(datas));
                partyView.SetText("武器一覧");
                SetOnClickItemButtons();
            }).AddTo(this);

        partyView.OnAnimal1
            .Subscribe(_ =>
            {
                partyView.HideAnimals();
                var datas = userDataModel.GetOwnedAnimals();
                partyView.ShowAnimals(datas,animalMasterData.GetSprites(datas));//enumがアクセサリーのもののみ渡す
                partyView.SetText("動物一覧");
                SetOnClickAnimal1Buttons();
            }).AddTo(this);

        partyView.OnAnimal2
            .Subscribe(_ =>
            {
                partyView.HideAnimals();
                var datas = userDataModel.GetOwnedAnimals();
                partyView.ShowAnimals(datas, animalMasterData.GetSprites(datas));//enumがアクセサリーのもののみ渡す
                partyView.SetText("動物一覧");
                SetOnClickAnimal2Buttons();
            }).AddTo(this);
    }

    public void SetOnClickItemButtons()
    {
        foreach (var itemButton in partyView.itemButtons)
        {
            itemButton.GetButton().OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (itemButton.itemParam.itemType == ItemParam.ItemType.武器)
                    {
                        userDataModel.SetPartyBuki(itemButton.itemParam);
                        var party = userDataModel.GetPartyParam();
                        if (party.buki != null)
                            partyView.SetBuki(itemSpriteMasterData.GetSprite(party.buki.name));
                    }
                    else if (itemButton.itemParam.itemType == ItemParam.ItemType.防具)
                    {
                        userDataModel.SetPartyBougu(itemButton.itemParam);
                        var party = userDataModel.GetPartyParam();
                        if (party.bougu != null)
                            partyView.SetBougu(itemSpriteMasterData.GetSprite(party.bougu.name));

                    } else if (itemButton.itemParam.itemType == ItemParam.ItemType.アクセサリー)
                    {
                        userDataModel.SetPartyAkuse(itemButton.itemParam);
                        var party = userDataModel.GetPartyParam();
                        if (party.akuse != null)
                            partyView.SetAkusesari(itemSpriteMasterData.GetSprite(party.akuse.name));

                    }
                }).AddTo(this);
        }
    }

    public void SetOnClickAnimal1Buttons()
    {
        for (int i = 0; i < partyView.animalButtons.Count; i++)
        {
            var animalButton = partyView.animalButtons[i];
            var index = i;
            animalButton.GetButton().OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //前に選んだものを選択できるようにする
                    var prevAnimal = userDataModel.GetPartyParam().animal1;
                    var ownAnimals = userDataModel.GetOwnedAnimals();
                    if (prevAnimal == null)
                    {
                        //スキップ
                    }
                    else
                    {
                        if (prevAnimal.name != AnimalParam.ANIMAL_NAME.None)
                        {
                            for (int j = 0; j < ownAnimals.Count; j++)
                            {
                                var ownAnimal = ownAnimals[j];
                                if (ownAnimal.name == prevAnimal.name && ownAnimal.isParty)//TODO
                                {
                                    userDataModel.SetIsParty(false, j);
                                    var prevAnimalButton = partyView.animalButtons[j];
                                    prevAnimalButton.SetActiveProhibitImage(false);
                                    break;
                                }
                            }
                        }
                    }
                    userDataModel.SetPartyAnimal1(animalButton.GetAnimalParam());
                    userDataModel.SetIsParty(true,index);
                    //選んだものを選択できないようにする
                    animalButton.SetActiveProhibitImage(true);
                    var party = userDataModel.GetPartyParam();
                    if (party.animal1 != null)
                        partyView.SetAnimal1(animalMasterData.GetSprite(party.animal1.name));

                }).AddTo(this);
        }
    }

    public void SetOnClickAnimal2Buttons()
    {
        for (int i=0;i<partyView.animalButtons.Count;i++)
        {
            var animalButton = partyView.animalButtons[i];
            var index = i;
            animalButton.GetButton().OnClickAsObservable()
                .Subscribe(_ =>
                {
                    //前に選んだものを選択できるようにする
                    var prevAnimal = userDataModel.GetPartyParam().animal2;
                    var ownAnimals = userDataModel.GetOwnedAnimals();
                    if (prevAnimal == null)
                    {
                        //スキップ
                    }
                    else
                    {
                        for (int j = 0; j < ownAnimals.Count; j++)
                        {
                            var ownAnimal = ownAnimals[j];
                            if (ownAnimal.name == prevAnimal.name && ownAnimal.isParty)//TODO
                            {
                                userDataModel.SetIsParty(false, j);
                                var prevAnimalButton = partyView.animalButtons[j];
                                prevAnimalButton.SetActiveProhibitImage(false);
                                break;
                            }
                        }
                    }
                    userDataModel.SetPartyAnimal2(animalButton.GetAnimalParam());
                    userDataModel.SetIsParty(true, index);
                    animalButton.SetActiveProhibitImage(true);
                    var party = userDataModel.GetPartyParam();
                    if (party.animal2 != null)
                        partyView.SetAnimal2(animalMasterData.GetSprite(party.animal2.name));

                }).AddTo(this);
        }
    }
}
