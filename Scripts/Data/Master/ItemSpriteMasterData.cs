using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム名に対応した画像に関するマスターデータ
/// </summary>
[CreateAssetMenu(fileName = "ItemSpriteMasterData", menuName = "Data/ItemSpriteMasterData")]
public class ItemSpriteMasterData : ScriptableObject
{
    public List<DictPair<ItemParam.ItemName, Sprite>> datas;
    public Sprite GetSprite(ItemParam.ItemName name)
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

    public List<Sprite> GetSprites(List<ItemParam> items)
    {
        List<Sprite> datas = new List<Sprite>();
        foreach (var item in items)
        {
            datas.Add(GetSprite(item.name));
        }
        return datas;
    }
}
