using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI内の各インベントリの要素の処理を記述するクラス
/// </summary>
public class AnimalButton : MonoBehaviour
{
    [SerializeField] private AnimalData animalData;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text animalName;
    [SerializeField] private Image image;
    //初期化
    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    public void Init(AnimalData animalData)
    {
        this.animalData = animalData;
        animalName.text = animalData.name;
        if (animalData.sprite != null)
            image.sprite = animalData.sprite;
    }

    /// <summary>
    /// ボタンが押された時の処理
    /// </summary>
    private void OnClick()
    {
        //Debug.Log(1);
    }
}
