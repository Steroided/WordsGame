using Systems.SceneManagement;
using UnityEngine;
using Zenject;

public class OptionsInstaller : MonoInstaller
{
    [SerializeField]
    private SoundManager _manager;
    public override void InstallBindings()
    {
        Container.Bind<SoundManager>().FromInstance(_manager);
        Container.Bind<SoundManagerButtonsProvider>().FromNew().AsSingle();
    }
}