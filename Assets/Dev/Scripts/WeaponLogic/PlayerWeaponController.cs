using System;
using Dev.BotLogic;
using Dev.PauseLogic;
using Dev.ScoreLogic;
using Dev.StaticData;
using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class PlayerWeaponController : WeaponController, IPauseListener
    {
        private InputProvider _inputProvider;
        private ScoreService _scoreService;
        private GameConfig _gameConfig;
        private bool _isGamePaused;

        [Inject]
        private void Construct(InputProvider inputProvider, ScoreService scoreService, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _scoreService = scoreService;
            _inputProvider = inputProvider;
        }

        private void Awake()
        {
            PauseService.Instance.RegisterListener(this);
        }

        private void OnDestroy()
        {
            PauseService.Instance.RemoveListener(this);
        }

        protected override void OnAmmoDied(AmmoDieContext ammoDieContext, Weapon weapon)
        {
            base.OnAmmoDied(ammoDieContext, weapon);

            bool isBot = ammoDieContext.Target.TryGetComponent<Bot>(out var bot);
            
            if (isBot)
            {
                bool getData = _gameConfig.BotConfig.TryGetData(bot.BotTag, out var staticData);

                if (getData)
                {
                    _scoreService.AddScore(staticData.Reward);
                }

                bot.ToDie.OnNext(BotDieReason.ByPlayer);
            }
        }

        private void Update()
        {
            if(_isGamePaused) return;
            
            if (_inputProvider.ToShoot)
            {
                Shoot(Vector2.up);
            }
        }

        public void OnPause(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }
    }
}