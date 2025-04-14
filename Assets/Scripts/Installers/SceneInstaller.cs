using Systems.SceneManagement;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private RemoteConfigLoader _remoteConfigLoader;
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(_sceneLoader);
        Container.Bind<RemoteConfigLoader>().FromInstance(_remoteConfigLoader);
        //Container.Bind<SoundManagerButtonsProvider>().FromNew().AsSingle();
    }
}