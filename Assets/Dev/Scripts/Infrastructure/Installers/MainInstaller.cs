using Dev.BotLogic;
using Dev.PlayerLogic;
using Dev.ScoreLogic;
using Dev.UI;
using UnityEngine;
using Zenject;

namespace Dev.Infrastructure
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private MenuService _menuService;

        [SerializeField] private BotsSpawner _botsSpawner;
        
        [SerializeField] private ScoreView _scoreView;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BotSpawnContext, Bot, BotFactory>().FromFactory<BotIFactory>();

            Container.Bind<BotsSpawner>().FromInstance(_botsSpawner).AsSingle();
            Container.Bind<ScoreService>().AsSingle().WithArguments(_scoreView);
            
            Container.Bind<MenuService>().FromInstance(_menuService).AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMovementController>().AsSingle().NonLazy();

            Container.Bind<CameraService>().FromInstance(_cameraService).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
        }
    }
}