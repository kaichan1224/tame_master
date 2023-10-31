using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using System;
using VContainer;

public class ActionSelectPanel : MonoBehaviour
{
    [SerializeField] private Button player1A;
    [SerializeField] private Button player1B;
    [SerializeField] private Button player2A;
    [SerializeField] private Button player2B;
    [SerializeField] private Button player3A;
    [SerializeField] private Button player3B;
    [SerializeField] private TMP_Text player1AText;
    [SerializeField] private TMP_Text player1BText;
    [SerializeField] private TMP_Text player2AText;
    [SerializeField] private TMP_Text player2BText;
    [SerializeField] private TMP_Text player3AText;
    [SerializeField] private TMP_Text player3BText;
    [SerializeField] private Image player1;
    [SerializeField] private Image player2;
    [SerializeField] private Image player3;
    public IObservable<Unit> Onplayer1A => player1A.OnClickAsObservable();
    public IObservable<Unit> Onplayer1B => player1B.OnClickAsObservable();
    public IObservable<Unit> Onplayer2A => player2A.OnClickAsObservable();
    public IObservable<Unit> Onplayer2B => player2B.OnClickAsObservable();
    public IObservable<Unit> Onplayer3A => player3A.OnClickAsObservable();
    public IObservable<Unit> Onplayer3B => player3B.OnClickAsObservable();
    public void Init(List<BattleParam> playerBattleParams,List<Sprite> sprites)
    {
        player1AText.text = playerBattleParams[0].actionType1.ToString();
        player1BText.text = playerBattleParams[0].actionType2.ToString();
        player2AText.text = playerBattleParams[1].actionType1.ToString();
        player2BText.text = playerBattleParams[1].actionType2.ToString();
        player3AText.text = playerBattleParams[2].actionType1.ToString();
        player3BText.text = playerBattleParams[2].actionType2.ToString();
        player1.sprite = sprites[0];
        player2.sprite = sprites[1];
        player3.sprite = sprites[2];
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
