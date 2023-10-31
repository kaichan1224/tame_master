using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AnimalParamのマスタデータ
/// </summary>
[CreateAssetMenu(fileName = "AnimalParamMasterData", menuName = "Data/AnimalParamMasterData")]
public class AnimalParamMasterData : ScriptableObject
{
    public List<AnimalParam> datas;
    public AnimalParam GetAnimalParam(AnimalParam.ANIMAL_NAME name)
    {
        return datas.Find(x => x.name == name);
    }


}
