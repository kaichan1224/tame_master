using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Reflection;
using UnityEngine.SceneManagement;

public class GachaModel
{
    private ItemMasterData itemMasterData;
    private UserDataModel userDataModel;

    [Inject]
    public GachaModel(ItemMasterData itemMasterData,UserDataModel userDataModel)
    {
        this.itemMasterData = itemMasterData;
        this.userDataModel = userDataModel;
    }

    public ItemParam GetAndDoGacha()
    {
        var item = GachaRandom();
        userDataModel.AddItem(item);
        userDataModel.DecrementGachaTicket(1);
        return item;
    }

    ItemParam GachaRandom()
    {

        var sum = Enum.GetValues(typeof(ItemParam.Rarity)).Cast<ItemParam.Rarity>().Sum(value => (int)value);
        Debug.Log(sum);
        var randomPoint = UnityEngine.Random.Range(0, sum);
        Debug.Log(randomPoint);
        foreach (ItemParam.Rarity rarity in Enum.GetValues(typeof(ItemParam.Rarity)))
        {
            if (randomPoint < (int)rarity)
                return RandomGetItem(rarity);
            randomPoint -= (int)rarity;
        }
        //ここを通ることはない?
        return RandomGetItem(ItemParam.Rarity.SR);
    }

    ItemParam RandomGetItem(ItemParam.Rarity rarity)
    {
        return itemMasterData.datas.Where(i => i.rarity== rarity).OrderBy(_ => System.Guid.NewGuid()).FirstOrDefault();
    }
}
