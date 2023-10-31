using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BattleLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<AttackModel>(Lifetime.Singleton);
        builder.Register<InGameBattleState>(Lifetime.Singleton);
    }
}
