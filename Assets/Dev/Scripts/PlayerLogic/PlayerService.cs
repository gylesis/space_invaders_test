using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class PlayerService : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;

        private Player _player;
        private PlayerMovementController _playerMovementController;

        public Subject<Unit> PlayerZeroHealth { get; } = new Subject<Unit>();

        [Inject]
        private void Construct(Player player, PlayerMovementController playerMovementController)
        {
            _playerMovementController = playerMovementController;
            _player = player;
        }

        private void Start()
        {
            _player.Health.ZeroHealth.TakeUntilDestroy(this).Subscribe(PlayerZeroHealth);
        }

        public void SetForbidInput(bool isInputDisabled)
        {
            _playerMovementController.IsInputDisabled = isInputDisabled;
        }

        public void ReloadWeapon()
        {
            _player.PlayerWeaponController.Reload();
        }
        
        public void RestoreHealth()
        {
            _player.Health.ResetHealth();
        }

        public void PlacePlayerToStart()
        {
            _player.transform.position = _startPoint.position;
        }
    }
}