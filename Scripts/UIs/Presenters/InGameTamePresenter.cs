using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine.XR.LegacyInputHelpers;

public class InGameTamePresenter : MonoBehaviour
{
    private TameModel tameModel;
    private bool isClick;
    [SerializeField] private InGameTameView inGameTameView;
    private UserDataModel userDataModel;
    private TameSceneViewState tameSceneViewState;

    [Inject]
    public void Init(TameModel tameModel,TameSceneViewState tameSceneViewState,UserDataModel userDataModel)
    {
        this.tameModel = tameModel;
        this.tameSceneViewState = tameSceneViewState;
        this.userDataModel = userDataModel;
    }

    void Start()
    {
        Bind();
    }

    void Bind()
    {
        userDataModel.kinomiNum
            .Subscribe(value =>
            {
                inGameTameView.SetEsaButtonText(value);
                if(value == 0)
                    inGameTameView.InActiveEsa();
            }).AddTo(this);

        inGameTameView.OnTap
            .Subscribe(_ =>
            {
                isClick = true;
            })
            .AddTo(this);

        inGameTameView.OnTame
        .Subscribe(_ =>
        {
            tameModel.SetCurrentState(TameState.タイミング);
        }).AddTo(this);

        inGameTameView.OnEsa
        .Subscribe(_ =>
        {
            tameModel.SetCurrentState(TameState.餌あげ);
        }).AddTo(this);

        tameModel.currentState
            .Subscribe(state =>
            {
                if (state == TameState.開始前)
                {
                    isClick = false;
                    tameModel.CreateTameAnimal();
                    inGameTameView.SetActiveTame(false);
                    inGameTameView.SetActiveSelect(false);
                    CountDown();
                    tameModel.SetTargetAreaValue(false);
                    var targetArea = tameModel.GetTargetAreaValue();
                    inGameTameView.SetTargetArea(targetArea.x,targetArea.y);
                }
                if (state == TameState.タイミング)
                {
                    inGameTameView.SetActiveSelect(false);
                    inGameTameView.SetActiveTame(true);
                    StartCoroutine(Taming());
                }
                if (state == TameState.失敗)
                {
                    tameSceneViewState.ChangeView(TameSceneViewState.View.FailView);
                }
                if (state == TameState.成功)
                {
                    //成功Viewへ
                    tameModel.SuccessTame();
                    tameSceneViewState.ChangeView(TameSceneViewState.View.ClearView);
                }
                if (state == TameState.選択)
                {
                    inGameTameView.SetActiveSelect(true);
                }
                if (state == TameState.餌あげ)
                {
                    StartCoroutine(Kinomi());
                }

            })
            .AddTo(this);

        // 画面の状態を監視して画面を表示・非表示
        tameSceneViewState.currentView
            .Subscribe(view =>
            {
                if (view == TameSceneViewState.View.TameView)
                {
                    inGameTameView.Show();
                }
                else
                {
                    inGameTameView.Hide();
                }
            })
            .AddTo(gameObject);
    }

    IEnumerator Kinomi()
    {
        inGameTameView.SetActiveSelect(false);
        userDataModel.SetKinomi(userDataModel.GetKinomiNum() - 1);
        var R = UnityEngine.Random.Range(1, 10);
        if (R >= 5)
        {
            inGameTameView.SetEsaText("テイム成功率アップ");
            tameModel.SetTargetAreaValue(true);
        }
        else
        {
            inGameTameView.SetEsaText("効果はなかった");
        }
        yield return new WaitForSeconds(1f);
        inGameTameView.SetActiveSelect(true);
        inGameTameView.SetEsaText("");
        var targetArea = tameModel.GetTargetAreaValue();
        inGameTameView.SetTargetArea(targetArea.x, targetArea.y);
        inGameTameView.InActiveEsa();
        tameModel.SetCurrentState(TameState.選択);
    }

    IEnumerator Taming()
    {
        int num = 0;
        int direction = 1;
        while (isClick == false)
        {
            num += UnityEngine.Random.Range(1, 10) * direction;
            if (num >= 100)
                direction = -1;
            if (num <= 0)
                direction = 1;
            inGameTameView.SetCurrentNatukido(num);
            yield return new WaitForSeconds(0.05f);
        }
        if (tameModel.IsSuccess(num))
        {
            tameModel.SetCurrentState(TameState.成功);
        }
        else
        {
            tameModel.SetCurrentState(TameState.失敗);
        }
    }

    public void CountDown()
    {
        var sequence = DOTween.Sequence();
        sequence
            .AppendCallback(() => inGameTameView.SetCountDownText("3"))
            .AppendInterval(1f)
            .AppendCallback(() => inGameTameView.SetCountDownText("2"))
            .AppendInterval(1f)
            .AppendCallback(() => inGameTameView.SetCountDownText("1"))
            .AppendInterval(1f)
            .AppendCallback(() => inGameTameView.SetCountDownText("Start"))
            .AppendInterval(0.5f)
            .OnComplete(() =>
            {
                inGameTameView.SetCountDownText("");
                tameModel.SetCurrentState(TameState.選択);
            });
    }
}
