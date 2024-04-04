using Dev.PauseLogic;

namespace Dev.Scripts.UI.PopUpsAndMenus
{
    public class PauseMenu : Menu
    {
        protected override void Awake()
        {
            base.Awake();
            
            OnSucceedButtonClicked(OnPauseButtonClicked);
        }

        private void OnPauseButtonClicked()
        {
            PauseService.Instance.SetPause(false);
            
            MenuService.HideMenu<PauseMenu>();
            MenuService.ShowMenu<InGameMenu>();
        }
        
    }
}