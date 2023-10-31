using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class AchieveButton : MonoBehaviour
{
    [SerializeField] private TMP_Text achieveExplain;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Image image;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject clearImageObj;
    public void Init(AchievemParam achievemParam,Sprite achieveSprite)
    {
        achieveExplain.text = achievemParam.achieveType.ToString();
        slider.value = (float)achievemParam.currentprogress / (float)achievemParam.maxprogress;
        progressText.text = $"{achievemParam.currentprogress}/{achievemParam.maxprogress}";
        rewardText.text = achievemParam.rewardType.ToString();
        image.sprite = achieveSprite;
        if (achievemParam.isAchieved)
            ActivateClearImage();
    }

    public void ActivateClearImage()
    {
        clearImageObj.SetActive(true);
    }
}
