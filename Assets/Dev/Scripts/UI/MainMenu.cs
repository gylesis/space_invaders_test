using Dev.Infrastructure;
using Zenject;

namespace Dev.Scripts.UI.PopUpsAndMenus
{
    public class MainMenu : Menu
    {
        private GameStateService _gameStateService;

        protected override void Awake()
        {
            OnSucceedButtonClicked((() =>
            {
                MenuService.HideMenu<MainMenu>();
                
                _gameStateService.StartGame();
            }));
        }

        [Inject]
        private void Construct(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
    }
}