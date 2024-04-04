using Dev.PlayerLogic;
using Dev.StaticData;
using UnityEngine;
using Zenject;

namespace Dev.Infrastructure
{
    public class SpaceInvadersProjectContext : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputProvider>().AsSingle().NonLazy();

            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
        }
    }
}