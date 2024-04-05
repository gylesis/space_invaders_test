using System;
using System.Collections.Generic;
using Dev.PauseLogic;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Dev.BotLogic
{
    public class BotGroupController : MonoBehaviour, IPauseListener
    {
        [SerializeField] private float _shootCooldown = 3f;

        [SerializeField] private float _moveCooldown = 1f;
        [SerializeField] private float _moveStep = 5f;

        [SerializeField] private Vector2 _moveDirection;

        [SerializeField] private int _moveStepsHorizontal = 8;

        [SerializeField] private AnimationCurve _moveDiffucultyCurve;

        private float _moveDifficultyModifier = 1f;

        private int _currentStepsMoved = 0;

        private bool _needToStepDown;

        private float _timer;

        private List<Bot> _bots = new List<Bot>();

        private bool _isGamePaused;
        private BotFactory _botFactory;
        private BotsSpawner _botsSpawner;

        private float _moveTimer;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _shootDisposable = new CompositeDisposable();

        private void Awake()
        {
            _moveDirection = Vector2.left;

            PauseService.Instance.RegisterListener(this);
        }

        private void OnDestroy()
        {
            PauseService.Instance.RemoveListener(this);
        }

        public void OnPause(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }

        [Inject]
        private void Construct(BotsSpawner botsSpawner)
        {
            _botsSpawner = botsSpawner;
        }

        public void StopControlling()
        {
            _shootDisposable?.Clear();
            _compositeDisposable?.Clear();
        }

        public void SetupBots()
        {
            StopControlling();

            foreach (var bot in _botsSpawner.SpawnedBots)
            {
                RegisterBot(bot);
            }

            Observable
                .EveryUpdate()
                .Where((l => _isGamePaused == false))
                .TakeUntilDestroy(this)
                .Subscribe((l =>
                {
                    _moveTimer += Time.deltaTime;

                    if (_moveTimer >= _moveCooldown / _moveDifficultyModifier)
                    {
                        _moveTimer = 0;
                        MoveGroup();
                    }
                })).AddTo(_compositeDisposable);

            Observable
                .Interval(TimeSpan.FromSeconds(_shootCooldown))
                .Where((l => _isGamePaused == false))
                .TakeUntilDestroy(this)
                .Subscribe((l => { TrySomeoneToShoot(); })).AddTo(_compositeDisposable);
        }

        public void RegisterBot(Bot bot)
        {
            bot.ToDie.TakeUntilDestroy(this).Subscribe((dieReason => OnBotToDie(bot, dieReason)));
            _bots.Add(bot);
        }

        public void UnRegisterBot(Bot bot)
        {
            _bots.Remove(bot);
        }

        private void OnBotToDie(Bot bot, BotDieReason dieReason)
        {
            UnRegisterBot(bot);

            if (dieReason == BotDieReason.ByPlayer)
            {
                float modifier = 1 +
                                 _moveDiffucultyCurve.Evaluate((float)_botsSpawner.DiedBotsAmount /
                                                               _botsSpawner.WasSpawnedBotsAmount);
                //Debug.Log($"Difficulty modifier {modifier}");

                _moveDifficultyModifier = modifier;
            }
        }

        private void TrySomeoneToShoot()
        {
            int botsAmountToShoot = Random.Range(1, 4);

            for (int i = 0; i < botsAmountToShoot; i++)
            {
                Bot bot = _bots[Random.Range(0, _bots.Count)];

                Observable.Timer(TimeSpan.FromSeconds(Random.Range(0f, 0.3f))).Subscribe((l =>
                {
                    bot.WeaponController.SelectWeapon(Random.Range(0, bot.WeaponController.WeaponCount));
                    bot.WeaponController.Shoot(Vector2.down);
                })).AddTo(_shootDisposable);
            }
        }

        private void MoveGroup()
        {
            Vector2 moveDirection = _moveDirection;

            if (_currentStepsMoved >= _moveStepsHorizontal)
            {
                _currentStepsMoved = 0;
                moveDirection = Vector2.down;
                _moveDirection.x = -_moveDirection.x;
            }

            foreach (Bot bot in _bots)
            {
                bot.MovementController.Move(moveDirection, _moveStep);
            }

            _currentStepsMoved++;
        }
    }
}