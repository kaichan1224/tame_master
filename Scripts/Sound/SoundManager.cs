using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音の管理を行うシングルトンクラス
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //AudioSource
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource SEAudio;
    //データ
    [SerializeField] private SoundMasterData soundMasterData;
    public void PlayBGM(SoundName soundName)
    {
        bgmAudio.clip = soundMasterData.GetSound(soundName);
        bgmAudio.Play();
    }

    public void StopBGM()
    {
        bgmAudio.Stop();
    }

    public void PlaySE(SoundName soundName)
    {
        SEAudio.PlayOneShot(soundMasterData.GetSound(soundName));
    }
}