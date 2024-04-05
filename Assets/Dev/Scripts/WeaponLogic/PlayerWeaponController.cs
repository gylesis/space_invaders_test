using Dev.BotLogic;
using Dev.ScoreLogic;
using Dev.StaticData;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class PlayerWeaponController : WeaponController
    {
        private InputProvider _inputProvider;
        private ScoreService _scoreService;
        private GameConfig _gameConfig;

        [Inject]
        private void Construct(InputProvider inputProvider, ScoreService scoreService, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _scoreService = scoreService;
            _inputProvider = inputProvider;
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

                bot.ToDie.OnNext(Unit.Default);
            }
        }

        private void Update()
        {
            if (_inputProvider.ToShoot)
            {
                Shoot(Vector2.up);
            }
        }
    }
}