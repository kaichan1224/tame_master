using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using System;

/// <summary>
/// UI内の各インベントリの要素の処理を記述するクラス
/// </summary>
public class AnimalButton : MonoBehaviour
{
    //UI
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text animalName;
    [SerializeField] private Image image;
    [SerializeField] private GameObject prohibitObj;
    private AnimalParam animalParam;
    public IObservable<Unit> OnClick => button.OnClickAsObservable();
    public void Init(AnimalParam animalParam,Sprite animalSprite)
    {
        this.animalParam = animalParam;
        image.sprite = animalSprite;
        animalName.text = animalParam.name.ToString();
    }

    public Button GetButton()
    {
        return button;
    }

    public AnimalParam GetAnimalParam()
    {
        return animalParam;
    }

    public void SetActiveProhibitImage(bool flag)
    {
        prohibitObj.SetActive(flag);
        button.interactable = !flag;
    }
}
