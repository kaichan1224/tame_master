using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchieveSpriteMasterData", menuName = "Data/AchieveSpriteMasterData")]
public class AchieveSpriteMasterData : ScriptableObject
{
    public List<DictPair<AchievemParam.AchieveType, Sprite>> datas;
    public Sprite GetSprite(AchievemParam.AchieveType name)
    {
        foreach (var data in datas)
        {
            if (data.IsEquelKey(name))
            {
                return data.Value;
            }
        }
        return null;
    }


}
