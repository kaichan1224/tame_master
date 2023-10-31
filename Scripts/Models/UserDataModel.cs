using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using System.Linq;
/// <summary>
/// ユーザーデータの更新の獲得及び更新、UIへの通知を行う
/// </summary>
public class UserDataModel
{
    //Model
    private SaveDataRepository saveDataRepository;
    private AchieveSpriteMasterData achieveSpriteMasterData;
    
    //各パラメータ
    public ReactiveProperty<int> level = new();
    public ReactiveProperty<int> exp = new();
    public ReactiveProperty<int> tameCnt = new();
    public ReactiveProperty<double> distanceTraveled = new();
    public ReactiveProperty<double> totalKcal = new();
    public ReactiveProperty<string> name = new();
    public List<AnimalParam> ownedAnimals;
    public List<ItemParam> ownedItems;
    public ReactiveProperty<PartyParam> party = new();
    public List<AchievemParam> ownedAchieves;
    public ReactiveProperty<StatusParam> playerStatus = new();
    public ReactiveProperty<int> gachaTicketNum = new();
    public string latestAchieveUpdateDay;
    public bool isFinishTutorial;
    public ReactiveProperty<int> kinomiNum = new();
    //選択Mode
    public BattleType battleType;

    [Inject]
    public UserDataModel(SaveDataRepository saveDataRepository,AchieveSpriteMasterData achieveSpriteMasterData)
    {
        this.saveDataRepository = saveDataRepository;
        this.achieveSpriteMasterData = achieveSpriteMasterData;
        var userParam = saveDataRepository.Load();
        level.Value = userParam.level;
        exp.Value = userParam.exp;
        tameCnt.Value = userParam.tameCnt;
        distanceTraveled.Value = userParam.distanceTraveled;
        totalKcal.Value = userParam.totalKcal;
        name.Value = userParam.name;
        ownedAnimals = userParam.ownedAnimals;
        ownedItems = userParam.ownedItems;
        party.Value = userParam.party;
        ownedAchieves = userParam.ownedAchieves;
        playerStatus.Value = userParam.playerStatus;
        gachaTicketNum.Value = userParam.gachaTicketNum;
        latestAchieveUpdateDay = userParam.latestAchieveUpdateDay;
        isFinishTutorial = userParam.isFinishTutorial;
        kinomiNum.Value = userParam.kinomiNum;
    }

    public void SetKinomi(int num)
    {
        kinomiNum.Value = num;
    }

    public int GetKinomiNum()
    {
        return kinomiNum.Value;
    }

    public void SetUpdateDay(string day)
    {
        latestAchieveUpdateDay = day;
        Save();
    }

    public void SetName(string name)
    {
        this.name.Value = name;
        Save();
    }

    public void FinishTutorial()
    {
        isFinishTutorial = true;
        Save();
    }

    public void DecrementGachaTicket(int num)
    {
        gachaTicketNum.Value -= num;
        Save();
    }

    public void IncrementGachaTicket(int num)
    {
        gachaTicketNum.Value += num;
        Save();
    }

    public void AddItem(ItemParam itemParam)
    {
        ownedItems.Add(itemParam);
        Save();
    }

    public void AddAnimal(AnimalParam animalParam)
    {
        ownedAnimals.Add(animalParam);
        Save();
    }

    public void SetPartyBuki(ItemParam itemParam)
    {
        party.Value.buki = itemParam;
        Save();
    }

    public void SetPartyAkuse(ItemParam itemParam)
    {
        party.Value.akuse = itemParam;
        Save();
    }

    public void SetPartyBougu(ItemParam itemParam)
    {
        party.Value.bougu = itemParam;
        Save();
    }

    public void SetPartyAnimal1(AnimalParam animalParam)
    {
        party.Value.animal1 = animalParam;
        Save();
    }

    public void SetPartyAnimal2(AnimalParam animalParam)
    {
        party.Value.animal2 = animalParam;
        Save();
    }

    public void UpdateLevel()
    {
        level.Value++;
        var nextStatus = playerStatus.Value;
        nextStatus.Attack += 2;
        nextStatus.Hp += 2;
        nextStatus.Defense += 2;
        nextStatus.Speed += 2;
        Save();
    }

    public void UpdateExp(int deltaAmount)
    {
        exp.Value += deltaAmount;
        while(NeedExp(level.Value) <= exp.Value)
        {
            exp.Value = exp.Value - NeedExp(level.Value);
            UpdateLevel();
        }
        Save();
    }

    private void SetStatus(StatusParam newStatus)
    {
        playerStatus.Value = newStatus;
        Save();
    }

    public int NeedExp(int level)
    {
        return level * 100;
    }

    /// <summary>
    /// 指定されたタイプのアチーブメントの数値を指定量だけ更新し、報酬を獲得する
    /// </summary>
    /// <param name="achieveType"></param>
    /// <param name="deltaAmount"></param>
    public void UpdateAchieve(AchievemParam.AchieveType achieveType,int deltaAmount)
    {
        for (int i = 0; i < ownedAchieves.Count; i++)
        {
            if (ownedAchieves[i].achieveType == achieveType && ownedAchieves[i].isAchieved == false)
            {
                ownedAchieves[i].currentprogress += deltaAmount;
                if (ownedAchieves[i].currentprogress >= ownedAchieves[i].maxprogress)
                {
                    ownedAchieves[i].isAchieved = true;
                    switch (ownedAchieves[i].rewardType)
                    {
                        case AchievemParam.RewardType.ガチャチケ1枚:
                            IncrementGachaTicket(1);
                            break;
                        case AchievemParam.RewardType.ガチャチケ3枚:
                            IncrementGachaTicket(3);
                            break;
                        case AchievemParam.RewardType.ガチャチケ5枚:
                            IncrementGachaTicket(5);
                            break;
                        case AchievemParam.RewardType.経験値100:
                            UpdateExp(100);
                            break;
                        case AchievemParam.RewardType.経験値300:
                            UpdateExp(300);
                            break;
                        case AchievemParam.RewardType.経験値500:
                            UpdateExp(500);
                            break;
                        case AchievemParam.RewardType.木の実3個:
                            SetKinomi(GetKinomiNum() + 3);
                            break;
                    }
                }
            }
        }
        Save();
    }

    public void SetAchieves(List<AchievemParam> achievems)
    {
        ownedAchieves = achievems;
        Save();
    }

    public void SetIsParty(bool isParty, int index)
    {
        Debug.Log($"{isParty},{index}");
        ownedAnimals[index].isParty = isParty;
        Save();
    }

    public PartyParam GetPartyParam()
    {
        return party.Value;
    }

    public List<AnimalParam> GetOwnedAnimals()
    {
        return ownedAnimals;
    }

    public List<ItemParam> GetOwnedItems(ItemParam.ItemType itemType)
    {
        return ownedItems.Where(value => value.itemType == itemType).ToList();
    }

    public List<AchievemParam> GetOwnedAchieves()
    {
        return ownedAchieves;
    }

    public List<Sprite> GetOwnedAchieveSprites()
    {
        List<Sprite> sprites = new List<Sprite>();
        foreach (var achieve in ownedAchieves)
        {
            sprites.Add(achieveSpriteMasterData.GetSprite(achieve.achieveType));
        }
        return sprites;
    }

    private void Save()
    {
        saveDataRepository.Save(new UserParam(
            distanceTraveled.Value,
            totalKcal.Value,
            exp.Value,
            level.Value,
            tameCnt.Value,
            name.Value,
            ownedAnimals,
            ownedItems,
            party.Value,
            ownedAchieves,
            playerStatus.Value,
            gachaTicketNum.Value,
            latestAchieveUpdateDay,
            isFinishTutorial,
            kinomiNum.Value
            ));
    }
}
