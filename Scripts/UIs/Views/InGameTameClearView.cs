using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class InGameTameClearView : MonoBehaviour
{
    [SerializeField] private Button backButton;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
