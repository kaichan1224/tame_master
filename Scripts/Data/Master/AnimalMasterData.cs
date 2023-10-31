using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "AnimalMasterData", menuName = "Data/AnimalMasterData")]
public class AnimalMasterData : ScriptableObject
{
    public List<AnimalMasterParam> datas;
    public Animal GetAnimal(AnimalParam.ANIMAL_NAME name)
    {
        return datas.Find(x => x.name == name).animal;
    }

    public Sprite GetSprite(AnimalParam.ANIMAL_NAME name)
    {
        var sprite = datas.Find(x => x.name == name).image;
        if (sprite)
            return datas.Find(x => x.name == name).image;
        else
        {
            Debug.Log("null");
            return null;
        }
    }

    public List<Sprite> GetSprites(List<AnimalParam> items)
    {
        List<Sprite> datas = new List<Sprite>();
        foreach (var item in items)
        {
            datas.Add(GetSprite(item.name));
        }
        return datas;
    }
}
