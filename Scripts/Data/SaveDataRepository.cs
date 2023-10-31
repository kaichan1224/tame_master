using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataRepository
{
    private const string USERDATA_PREF_KEY = "UserData";
    public void Save(UserParam userParam)
    {
        string savejson = JsonUtility.ToJson(userParam);
        PlayerPrefs.SetString(USERDATA_PREF_KEY, savejson);
        PlayerPrefs.Save();
    }

    public UserParam Load()
    {
        if (PlayerPrefs.HasKey(USERDATA_PREF_KEY))
        {
            string loadJson = PlayerPrefs.GetString(USERDATA_PREF_KEY);
            return JsonUtility.FromJson<UserParam>(loadJson);
        }
        else
        {
            return NewData();
        }
        
    }

    public UserParam NewData()
    {
        double initialDistanceTraveled = 0.0;
        double initialTotalKcal = 0.0;
        int initialExp = 0;
        int initialLevel = 1;
        int initialTameCnt = 0;
        string initialName = "DefaultName";
        List<AnimalParam> initialOwnedAnimals = new List<AnimalParam>();
        List<ItemParam> initialOwnedItems = new List<ItemParam>();
        PartyParam party = new PartyParam();//TODO初期パーティを指定する必要あり
        List<AchievemParam> initialOwnedAchieves = new List<AchievemParam>();
        StatusParam initialStatusParam = new StatusParam(500,50,50,50);
        int initialGachaTicketNum = 20;
        string latestAchieveUpdateDay = "";
        bool isFinishTutorial = true;
        int kinomiNum = 3;
        return new UserParam(
            initialDistanceTraveled,
            initialTotalKcal,
            initialExp,
            initialLevel,
            initialTameCnt,
            initialName,
            initialOwnedAnimals,
            initialOwnedItems,
            party,
            initialOwnedAchieves,
            initialStatusParam,
            initialGachaTicketNum,
            latestAchieveUpdateDay,
            isFinishTutorial,
            kinomiNum
        );
    }
}
