using Dev.PlayerLogic;
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
        
        public override void InstallBindings()
        {
            Container.Bind<MenuService>().FromInstance(_menuService).AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMovementController>().AsSingle().NonLazy();

            Container.Bind<CameraService>().FromInstance(_cameraService).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
        }
    }
}