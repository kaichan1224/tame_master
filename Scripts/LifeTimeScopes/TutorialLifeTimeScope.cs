using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TutorialLifeTimeScope : LifetimeScope
{
    [SerializeField] TutorialMasterData tutorialMasterData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(tutorialMasterData);
    }
}
