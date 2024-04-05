using Dev.Infrastructure;
using Dev.PauseLogic;
using Dev.PlayerLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dev.UI
{
    public class InGameMenu : Menu
    {
        [SerializeField] private DefaultReactiveButton _shootButton;
        [SerializeField] private DefaultReactiveButton _pauseButton;

        private InputProvider _inputProvider;

        private void Start()
        {
            _shootButton.Clicked.TakeUntilDestroy(this).Subscribe((unit =>
            {
                _inputProvider.SimulateShoot();
            }));
            
            _pauseButton.Clicked.TakeUntilDestroy(MenuService).Subscribe((unit => OnPauseButtonClicked()));
        }

        [Inject]
        private void Construct(InputProvider inputProvider) 
        {
            _inputProvider = inputProvider;
        }
        
        private void OnPauseButtonClicked()
        {
            PauseService.Instance.SetPause(true);

            MenuService.HideMenu<InGameMenu>();
            MenuService.ShowMenu<PauseMenu>();
        }
    }
}