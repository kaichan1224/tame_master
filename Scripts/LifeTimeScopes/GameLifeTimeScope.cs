using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private LocationUpdater locationUpdater;
    [SerializeField] private MapCamera3dManager cameraManager;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(locationUpdater);
        builder.RegisterInstance(cameraManager);
        builder.Register<ViewState>(Lifetime.Singleton);
        builder.Register<GachaModel>(Lifetime.Singleton);
    }
}
