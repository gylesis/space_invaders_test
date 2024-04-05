using System;
using System.Threading.Tasks;
using Dev.BotLogic;
using Dev.PauseLogic;
using Dev.PlayerLogic;
using Dev.ScoreLogic;
using Dev.Scripts.UI.PopUpsAndMenus;
using Dev.UI;
using UniRx;
using Zenject;

namespace Dev.Infrastructure
{
    public class GameStateService : IInitializable, IDisposable
    {
        private ScoreService _scoreService;
        private BotsSpawner _botsSpawner;
        private PlayerService _playerService;
        private BotGroupController _botGroupController;
        private MenuService _menuService;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public GameStateService(ScoreService scoreService, BotsSpawner botsSpawner, PlayerService playerService,
            BotGroupController botGroupController, MenuService menuService)
        {
            _menuService = menuService;
            _botGroupController = botGroupController;
            _playerService = playerService;
            _botsSpawner = botsSpawner;
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            _botsSpawner.AllBotsDied.Subscribe((unit => OnAllBotsDied())).AddTo(_compositeDisposable);
            _playerService.PlayerZeroHealth.Subscribe((unit => OnPlayerDied())).AddTo(_compositeDisposable);
        }

        private void OnAllBotsDied()
        {
            OnGameFinish(false);
        }

        public void OnPlayerDied()
        {
            OnGameFinish(true);
        }

        private void OnGameFinish(bool isLost)
        {
            _botGroupController.StopControlling();
            _playerService.SetForbidInput(true);
            
            PauseService.Instance.SetPause(true);
            
            _menuService.HideMenu<InGameMenu>();

            var tryGetMenu = _menuService.TryGetMenu<FinishMenu>(out var finishMenu);
            
            if (tryGetMenu)
            {
                finishMenu.Setup(isLost);
                _menuService.ShowMenu<FinishMenu>();
                finishMenu.OnSucceedButtonClicked((() =>
                {
                    _menuService.HideMenu<FinishMenu>();
                    RestartGame();
                }));
            }
        }
        
        public void RestartGame()
        {
            ResetGame();
            StartGame();
        }

        public void ResetGame()
        {   
            PauseService.Instance.SetPause(false);
            _playerService.SetForbidInput(true);
            _botsSpawner.UnSpawnAllBots();
            _scoreService.ResetScore();
        }

        public async Task StartGame()
        {
            _playerService.RestoreHealth();
            _playerService.PlacePlayerToStart();
            
            await _botsSpawner.SpawnBots();

            await Task.Delay(50);
            _botGroupController.SetupBots();
            _playerService.SetForbidInput(false);

            _menuService.ShowMenu<InGameMenu>();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();   
        }
    }
}