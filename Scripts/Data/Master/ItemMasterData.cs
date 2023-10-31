using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemMasterData", menuName = "Data/ItemMasterData")]
public class ItemMasterData : ScriptableObject
{
    public List<ItemParam> datas;
}
