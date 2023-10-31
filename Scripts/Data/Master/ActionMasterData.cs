using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

/// <summary>
/// 各アクションのマスタデータ作成
/// </summary>
[CreateAssetMenu(fileName = "ActionMasterData", menuName = "Data/ActionMasterData")]
public class ActionMasterData : ScriptableObject
{
    public List<DictPair<ActionType,ActionParam>> datas;
    public ActionParam GetAction(ActionType actionType)
    {
        foreach (var data in datas)
        {
            if (data.IsEquelKey(actionType))
            {
                return data.Value;
            }
        }
        return null;
    }
}
