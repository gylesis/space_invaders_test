using Dev.Infrastructure;
using Dev.PauseLogic;
using Dev.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dev.Scripts.UI.PopUpsAndMenus
{
    public class PauseMenu : Menu
    {
        [SerializeField] private DefaultReactiveButton _restartButton;
        
        private GameStateService _gameStateService;

        protected override void Awake()
        {
            base.Awake();

            _restartButton.Clicked.TakeUntilDestroy(this).Subscribe((unit => OnRestartButtonClicked()));
            OnSucceedButtonClicked(OnPauseButtonClicked);
        }

        [Inject]
        private void Construct(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
        private void OnRestartButtonClicked()
        {
            MenuService.HideMenu<PauseMenu>();
            MenuService.ShowMenu<InGameMenu>();
            
            _gameStateService.RestartGame();
        }

        private void OnPauseButtonClicked()
        {
            PauseService.Instance.SetPause(false);
            
            MenuService.HideMenu<PauseMenu>();
            MenuService.ShowMenu<InGameMenu>();
        }
        
    }
}