using Zenject;

namespace Dev.BotLogic
{
    public class BotInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Bot>().FromComponentOnRoot().AsSingle();

            Container.BindInterfacesAndSelfTo<BotMovementController>().AsSingle().NonLazy();
        }
    }
}