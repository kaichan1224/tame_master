using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;


public class InGameTameFailView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button backButton;
    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
