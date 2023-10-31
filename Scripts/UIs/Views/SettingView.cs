using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TMPro;

public class SettingView : MonoBehaviour
{
    //閉じるボタン
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Dropdown _dropdown;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public TMP_Dropdown dropdown => _dropdown;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
