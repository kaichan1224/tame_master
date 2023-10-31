using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class UserInfoView : MonoBehaviour
{
    //名前入力蘭
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text nameText;
    //閉じるボタン
    [SerializeField] private Button backButton;
    //レベルのテキスト
    [SerializeField] private TMP_Text levelText;
    //Expのテキスト
    [SerializeField] private TMP_Text expText;
    //ExpのSlider
    [SerializeField] private Slider expSlider;
    //総Kcalのテキスト
    [SerializeField] private TMP_Text totalKcalText;
    //総移動距離のテキスト
    [SerializeField] private TMP_Text moveDistanceText;
    //左上のアイコンの画像
    [SerializeField] private Image charactor;
    //プレイヤースタッツ
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenseText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text speedText;
    //武器・技
    [SerializeField] private TMP_Text weapon1NameText;
    [SerializeField] private TMP_Text weapon2NameText;
    [SerializeField] private TMP_Text weapon1ExpText;
    [SerializeField] private TMP_Text weapon2ExpText;

    public IObservable<Unit> OnBack => backButton.OnClickAsObservable();

    public void SetStatus(StatusParam statusParam)
    {
        attackText.text = statusParam.Attack.ToString();
        defenseText.text = statusParam.Defense.ToString();
        hpText.text = statusParam.Hp.ToString();
        speedText.text = statusParam.Speed.ToString();
    }

    public void SetWeapon(ItemParam weapon,ActionParam action1,ActionParam action2)
    {
        weapon1NameText.text = $"技1 {weapon.actionType1.ToString()}";
        weapon2NameText.text = $"技2 {weapon.actionType2.ToString()}";
        weapon1ExpText.text = $"技攻撃力{action1.skillAttack} 命中率{action1.hitRate}\n会心率{action1.criticalRate}　回復量{action1.healValue}";
        weapon2ExpText.text = $"技攻撃力{action2.skillAttack} 命中率{action2.hitRate}\n会心率{action2.criticalRate}　回復量{action2.healValue}";

    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetExp(int exp,int maxExp)
    {
        expText.text = exp.ToString() + "/" + maxExp.ToString();
        expSlider.value = (float)exp / (float)maxExp;
    }

    public void SetTotalKcal(double totalKcal)
    {
        totalKcalText.text = totalKcal.ToString();
    }

    public void SetTotalDistance(double totalDistance)
    {
        totalKcalText.text = totalDistance.ToString();
    }

    public void SetCharactorIcon(Sprite charactor)
    {
        this.charactor.sprite = charactor;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }


}
