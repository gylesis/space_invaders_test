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
        private bool _allowToShoot = true;

        private float _timer;

        private List<Bot> _bots = new List<Bot>();
        
        private bool _isGamePaused;
        private BotFactory _botFactory;
        private BotsSpawner _botsSpawner;

        private float _shootTimer;
        private float _moveTimer;
            
        private void Awake()
        {
            _moveDirection = Vector2.left;
            
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
                }));

            Observable
                .EveryUpdate()
                .Where((l => _isGamePaused == false))
                .TakeUntilDestroy(this)
                .Subscribe((l =>
                {
                    _shootTimer += Time.deltaTime;

                    if (_shootTimer >= _shootCooldown)
                    {
                        _shootTimer = 0;
                        TrySomeoneToShoot();
                    }
                }));

            PauseService.Instance.RegisterListener(this);
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

        private void Start()
        {
            List<Bot> bots = _botsSpawner.SpawnEnemies();
            
            foreach (var bot in bots)
            {
                RegisterBot(bot);
            }
        }

        public void RegisterBot(Bot bot)
        {
            bot.ToDie.TakeUntilDestroy(this).Subscribe((unit => OnBotToDie(bot)));
            _bots.Add(bot);
        }

        public void UnRegisterBot(Bot bot)
        {
            _bots.Remove(bot);
        }

        private void OnBotToDie(Bot bot)
        {
            UnRegisterBot(bot);
            _botsSpawner.UnSpawnBot(bot);

            float modifier = 1 + _moveDiffucultyCurve.Evaluate((float)_botsSpawner.DiedBotsAmount / _botsSpawner.WasSpawnedBotsAmount);

            Debug.Log($"Difficulty modifier {modifier}");
            _moveDifficultyModifier = modifier;
        }

        private void TrySomeoneToShoot()
        {
           // if (_allowToShoot == false) return;

            Bot bot = _bots[Random.Range(0, _bots.Count)];

            bot.WeaponController.Shoot(Vector2.down);

            _allowToShoot = false;
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