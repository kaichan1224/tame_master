using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class GachaPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private GachaView gachaView;
    //Model
    ViewState viewState;
    GachaModel gachaModel;
    UserDataModel userDataModel;
    ItemSpriteMasterData itemSpriteMasterData;

    [Inject]
    public void Init(ViewState viewState,GachaModel gachaModel,UserDataModel userDataModel,ItemSpriteMasterData itemSpriteMasterData)
    {
        this.viewState = viewState;
        this.gachaModel = gachaModel;
        this.userDataModel = userDataModel;
        this.itemSpriteMasterData = itemSpriteMasterData;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        userDataModel.gachaTicketNum
            .Subscribe(value =>
            {
                //1連続
                if (value >= 1)
                    gachaView.SetActiveGachaButton(true);
                else
                    gachaView.SetActiveGachaButton(false);
                //現在のチケット枚数表示部分
                gachaView.SetGachaTicketNumText(value);
            })
            .AddTo(this);

        gachaView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);

        gachaView.OnGacha
           .Subscribe(_ =>
           {
               StartCoroutine(DoGacha());
           })
           .AddTo(this);

        gachaView.OnBackResult
            .Subscribe(_ =>
            {
                gachaView.HideResultPanel();
            })
            .AddTo(this);
        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.GachaView)
                {
                    gachaView.Show();
                }
                else
                {
                    gachaView.Hide();
                }
            })
            .AddTo(gameObject);
    }

    IEnumerator DoGacha()
    {
        gachaView.SetActiveBrock(true);
        SoundManager.instance.PlaySE(SoundName.ガチャ引く);
        yield return new WaitForSeconds(1.5f);
        gachaView.SetActiveBrock(false);
        SoundManager.instance.PlaySE(SoundName.ガチャ出る);
        var item = gachaModel.GetAndDoGacha();
        Debug.Log(item.name);
        gachaView.SetResultPanel(item, itemSpriteMasterData.GetSprite(item.name));
        gachaView.ShowResultPanel();
    }


}
