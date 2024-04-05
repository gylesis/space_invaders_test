using Dev.BotLogic;
using Dev.ScoreLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class PlayerWeaponController : WeaponController
    {
        private InputProvider _inputProvider;
        private ScoreService _scoreService;

        [Inject]
        private void Construct(InputProvider inputProvider, ScoreService scoreService)
        {
            _scoreService = scoreService;
            _inputProvider = inputProvider;
        }

        protected override void OnAmmoDied(AmmoDieContext ammoDieContext, Weapon weapon)
        {
            base.OnAmmoDied(ammoDieContext, weapon);

            bool isBot = ammoDieContext.Target.TryGetComponent<Bot>(out var bot);
            
            if (isBot)
            {
                _scoreService.AddScore(100);

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