using System;
using System.Collections.Generic;
using Dev.PauseLogic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dev.BotLogic
{
    public class BotGroupController : MonoBehaviour, IPauseListener
    {
        [SerializeField] private List<Bot> _bots;

        [SerializeField] private float _shootCooldown = 3f;

        [SerializeField] private float _moveCooldown = 1f;
        [SerializeField] private float _moveStep = 5f;

        [SerializeField] private Vector2 _moveDirection;

        private bool _allowToShoot = true;

        private float _timer;

        private bool _isGamePaused;

        private void Awake()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(_moveCooldown))
                .Where((l => _isGamePaused == false))
                .TakeUntilDestroy(this)
                .Subscribe((l =>
                {
                    MoveGroup();
                }));

            Observable
                .Interval(TimeSpan.FromSeconds(_shootCooldown))
                .Where((l => _isGamePaused == false))
                .TakeUntilDestroy(this)
                .Subscribe((l =>
                {
                    //TrySomeoneToShoot();
                }));

            PauseService.Instance.RegisterListener(this);
        }

        public void OnPause(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }

        private void TrySomeoneToShoot()
        {
            if (_allowToShoot == false) return;

            Bot bot = _bots[Random.Range(0, _bots.Count)];

            bot.WeaponController.Shoot(Vector2.down);

            _allowToShoot = false;
        }

        private void MoveGroup()
        {
            foreach (Bot bot in _bots)
            {
                bot.MovementController.Move(_moveDirection, _moveStep);
            }
        }
    }
}