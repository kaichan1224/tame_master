using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

public class SettingPresenter : MonoBehaviour
{
    //UI
    [SerializeField] private SettingView settingView;
    //Model
    ViewState viewState;
    LocationUpdater locationUpdater;

    [Inject]
    public void Init(ViewState viewState,LocationUpdater locationUpdater)
    {
        this.viewState = viewState;
        this.locationUpdater = locationUpdater;
    }
    void Start()
    {
        Bind();
    }

    void Bind()
    {
        settingView.dropdown
            .onValueChanged.AsObservable()
            .Subscribe(index =>
            {
                var v = settingView.dropdown.options[index].text;
                // 正規表現パターンを定義します。
                string pattern = @"\d+"; // 1つ以上の数字にマッチする正規表現パターン
                // 正規表現にマッチする部分を抽出します。
                Match match = Regex.Match(v, pattern);
                int number = int.Parse(match.Value);
                Debug.Log(number);
                locationUpdater.SetIntervalTime(number);
            })
            .AddTo(this);
        //戻るボタン
        settingView.OnBack
            .Subscribe(_ =>
            {
                viewState.ChangeView(ViewState.View.Map3dView);
            })
            .AddTo(this);
        // 画面の状態を監視して画面を表示・非表示
        viewState.currentView
            .Subscribe(view =>
            {
                if (view == ViewState.View.SettingView)
                {
                    settingView.Show();
                }
                else
                {
                    settingView.Hide();
                }
            })
            .AddTo(gameObject);
    }
}
