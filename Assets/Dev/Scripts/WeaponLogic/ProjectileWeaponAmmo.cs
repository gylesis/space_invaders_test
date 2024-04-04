using Dev.Levels.Interactions;
using Dev.PauseLogic;
using Dev.Utils;
using UniRx;
using UnityEngine;

namespace Dev.PlayerLogic
{
    public abstract class ProjectileWeaponAmmo : WeaponAmmo, IPauseListener
    {
        [SerializeField] private TriggerZone _triggerZone;
        [SerializeField] private LayerMask _obstacleLayers;
        
        private ProjectileAmmoSetupContext _setupContext;
        private bool _isGamePaused;

        public Subject<Unit> ToDie { get; } = new Subject<Unit>();

        public void Setup(ProjectileAmmoSetupContext setupContext)
        {
            _setupContext = setupContext;
            transform.position = _setupContext.StartPos;
        }

        private void Awake()
        {
            _triggerZone.TriggerEntered.TakeUntilDestroy(this).Subscribe((OnProjectileTriggered));
        }

        private void OnProjectileTriggered(Collider2D collider)
        {
            if (_obstacleLayers.Contains(collider.gameObject.layer))
            {
                ToDie.OnNext(Unit.Default);
            }
        }

        private void FixedUpdate()
        {
            if(_isGamePaused) return;
            
            transform.position += _setupContext.Direction * (Time.deltaTime * _setupContext.Speed);
        }

        public void OnPause(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }
    }
}