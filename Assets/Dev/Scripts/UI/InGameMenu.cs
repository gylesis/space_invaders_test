using Dev.PauseLogic;
using Dev.UI;
using UniRx;
using UnityEngine;

namespace Dev.Scripts.UI.PopUpsAndMenus
{
    public class InGameMenu : Menu
    {
        [SerializeField] private DefaultReactiveButton _pauseButton;

        protected override void Awake()
        {
            
        }

        private void Start()
        {
            _pauseButton.Clicked.TakeUntilDestroy(MenuService).Subscribe((unit => OnPauseButtonClicked()));
        }

        private void OnPauseButtonClicked()
        {
            PauseService.Instance.SetPause(true);

            MenuService.HideMenu<InGameMenu>();
            MenuService.ShowMenu<PauseMenu>();
        }
    }
}