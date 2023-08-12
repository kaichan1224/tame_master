using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 効果音を再生するためのクラスx
/// </summary>
public class SoundPlay : MonoBehaviour
{
    //AudioSourceを入れる
    private AudioSource Audio;
    //曲再生の判定
    bool isAudioStart = false;
    //効果音の再生時間
    [SerializeField] private float destoryLimit;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        Audio = GetComponent<AudioSource>();//AudioSourceの取得
        Audio.Play();//AudioSourceを再生
        isAudioStart = true;//曲の再生を判定
    }
    void Update()
    {
        if (!Audio.isPlaying && isAudioStart)
        //曲が再生されていない、尚且つ曲の再生が開始されている時
        {
            Destroy(gameObject,destoryLimit);//オブジェクトを消す
        }
    }
}

