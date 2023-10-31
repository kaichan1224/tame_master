using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class BattleView : MonoBehaviour
{
    //閉じるボタン
    [SerializeField] private Button backButton;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Button veryhardButton;
    [SerializeField] private Button hellButton;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public IObservable<Unit> OnEasy => easyButton.OnClickAsObservable();
    public IObservable<Unit> OnNormal=> normalButton.OnClickAsObservable();
    public IObservable<Unit> OnHard => hardButton.OnClickAsObservable();
    public IObservable<Unit> OnVeryHard => veryhardButton.OnClickAsObservable();
    public IObservable<Unit> OnHell => hellButton.OnClickAsObservable();

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
