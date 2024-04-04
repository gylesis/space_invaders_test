using Dev.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Dev.Infrastructure
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private CameraService _cameraService;
        
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();

            Container.Bind<CameraService>().FromInstance(_cameraService).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
        }
    }
}