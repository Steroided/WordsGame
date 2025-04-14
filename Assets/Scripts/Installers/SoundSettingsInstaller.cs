using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SoundSettingsInstaller", menuName = "Installers/SoundSettingsInstaller")]
public class SoundSettingsInstaller : ScriptableObjectInstaller<SoundSettingsInstaller>
{
    [SerializeField]
    private SoundManagerSettings _soundSettings;


    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SoundManagerSettings>().FromInstance(_soundSettings).AsSingle();
    }
}