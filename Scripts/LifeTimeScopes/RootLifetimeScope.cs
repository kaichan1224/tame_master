using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] private ItemMasterData itemMasterData;
    [SerializeField] private AchieveMasterData achieveMasterData;
    [SerializeField] private ActionMasterData actionMasterData;
    [SerializeField] private AnimalParamMasterData animalParamMasterData;
    [SerializeField] private AchieveSpriteMasterData achieveSpriteMasterData;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AnimalMasterData animalMasterData;
    [SerializeField] private ItemSpriteMasterData itemSpriteMasterData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(soundManager);
        builder.RegisterInstance(itemMasterData);
        builder.RegisterInstance(itemSpriteMasterData);
        builder.RegisterInstance(achieveMasterData);
        builder.RegisterInstance(actionMasterData);
        builder.RegisterInstance(animalParamMasterData);
        builder.RegisterInstance(achieveSpriteMasterData);
        builder.RegisterInstance(animalMasterData);
        builder.Register<SaveDataRepository>(Lifetime.Singleton);
        builder.Register<UserDataModel>(Lifetime.Singleton);
        builder.Register<TameModel>(Lifetime.Singleton);
        builder.Register<AchieveModel>(Lifetime.Singleton);
    }
}