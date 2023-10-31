using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアル用のマスタデータ
/// </summary>
[CreateAssetMenu(fileName = "TutorialMasterData", menuName = "Data/TutorialMasterData")]
public class TutorialMasterData : ScriptableObject
{
    /// <summary>
    /// セリフデータ
    /// </summary>
    public List<TutorialParam> story;
}
