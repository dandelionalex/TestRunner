using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameCameraView gameCameraView;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<GameCameraView>().FromInstance(gameCameraView).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerUnitModel>().AsSingle();
        Container.DeclareSignal<ItemCollectedSignal>();
    }
}