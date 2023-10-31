using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundMasterData", menuName = "Data/SoundMasterData")]
public class SoundMasterData : ScriptableObject
{
    public List<DictPair<SoundName,AudioClip>> datas;
    public AudioClip GetSound(SoundName soundName)
    {
        foreach (var data in datas)
        {
            if (data.IsEquelKey(soundName))
            {
                return data.Value;
            }
        }
        return null;
    }
}
