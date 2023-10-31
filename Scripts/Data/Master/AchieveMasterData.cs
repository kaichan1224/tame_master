using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アチーブメントのマスタデータ
[CreateAssetMenu(fileName = "AchieveMasterData", menuName = "Data/AchieveMasterData")]
public class AchieveMasterData : ScriptableObject
{
    public List<AchievemParam> datas;
}
