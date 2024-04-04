using Dev.PlayerLogic;
using Dev.StaticData;
using UnityEngine;
using Zenject;

namespace Dev.Infrastructure
{
    public class SpaceInvadersProjectContext : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private WeaponStaticDataContainer _weaponStaticDataContainer;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputProvider>().AsSingle().NonLazy();

            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
            Container.Bind<WeaponStaticDataContainer>().FromInstance(_weaponStaticDataContainer).AsSingle();
        }
    }
}