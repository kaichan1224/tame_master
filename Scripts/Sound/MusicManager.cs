using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bgmを再生するためのクラス
/// </summary>
public class MusicManager : MonoBehaviour
{
    // AudioSourceをアタッチする必要あり
    [SerializeField] private AudioSource audioSource;
    //通常時のサウンド
    [SerializeField] private AudioClip idleSound;
    //戦闘時のサウンド
    [SerializeField] private AudioClip battleSound;
    //ゲームクリア時のサウンド
    [SerializeField] private AudioClip successSound;
    //テイム失敗時のサウンド
    [SerializeField] private AudioClip failSound;
    //進化時のサウンド
    [SerializeField] private AudioClip evoSound;

    //ゲーム開始時の処理
    void Start()
    {
        StartIdleBgm();
    }

    //bgmを停止する
    public void StopBgm()
    {
        audioSource.Stop();
    }

    //戦闘開始のサウンドを再生する
    public void StartBattleBgm()
    {
        audioSource.Stop();
        audioSource.clip = battleSound;
        audioSource.Play();
    }

    //通常時のサウンドを再生する
    public void StartIdleBgm()
    {
        audioSource.Stop();
        audioSource.clip = idleSound;
        audioSource.Play();
    }

    //ゲームクリアのサウンドを再生する
    public void StartSuccessBgm()
    {
        audioSource.Stop();
        audioSource.clip = successSound;
        audioSource.Play();
    }

    //テイム失敗のサウンドを再生する
    public void StartFailBgm()
    {
        audioSource.Stop();
        audioSource.clip = failSound;
        audioSource.Play();
    }

    //進化のサウンドを再生する
    public void StartEvoBgm()
    {
        audioSource.Stop();
        audioSource.clip = evoSound;
        audioSource.Play();
    }
}
