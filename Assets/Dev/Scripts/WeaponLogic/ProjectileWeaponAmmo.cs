using System;
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

        public Subject<AmmoDieContext> ToDie { get; } = new Subject<AmmoDieContext>();

        private void Start()
        {
            PauseService.Instance.RegisterListener(this);
        }

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
            if(_isGamePaused) return;
            
            if (_obstacleLayers.Contains(collider.gameObject.layer))
            {
                AmmoDieContext ammoDieContext = new AmmoDieContext();
                ammoDieContext.Target = collider.gameObject;
                ammoDieContext.Ammo = this;
                
                ToDie.OnNext(ammoDieContext);
                PauseService.Instance.RemoveListener(this);
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