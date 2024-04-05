using System;
using Dev.BotLogic;
using Dev.PauseLogic;
using Dev.PlayerLogic;
using Dev.Utils;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Dev.WeaponLogic
{
    public abstract class ProjectileWeaponAmmo : WeaponAmmo, IPauseListener
    {
        [SerializeField] private TriggerZone _triggerZone;
        [SerializeField] private LayerMask _obstacleLayers;

        protected ProjectileAmmoSetupContext _setupContext;
        private bool _isGamePaused;

        public Subject<AmmoDieContext> ToDie { get; } = new Subject<AmmoDieContext>();

        private void Start()
        {
            PauseService.Instance.RegisterListener(this);
        }

        private void OnDestroy()
        {
            PauseService.Instance.RemoveListener(this);
        }

        public void Setup(ProjectileAmmoSetupContext setupContext)
        {
            _setupContext = setupContext;

            transform.up = setupContext.Direction;
            transform.position = _setupContext.StartPos;
        }

        private void Awake()
        {
            _triggerZone.TriggerEntered.TakeUntilDestroy(this).Subscribe((OnProjectileTriggered));
        }

        private void OnProjectileTriggered(Collider2D collider)
        {
            if (_isGamePaused) return;

            bool isObstacle = _obstacleLayers.Contains(collider.gameObject.layer);

            if (_setupContext.IsOwnerPlayer)
            {
                bool isBot = collider.TryGetComponent<Bot>(out var bot);

                if (isBot)
                {
                    Die(collider.gameObject);
                    return;
                }
            }
            else
            {
                bool isPlayer = collider.TryGetComponent<Player>(out var player);

                if (isPlayer)
                {
                    Die(collider.gameObject); 
                    return;
                }
            }
                
            if (isObstacle)
            {
                Die(collider.gameObject); 
            }
        }

        private void Die(GameObject target)
        {   
            AmmoDieContext ammoDieContext = new AmmoDieContext();
            ammoDieContext.Target = target;
            ammoDieContext.Ammo = this;

            ToDie.OnNext(ammoDieContext);
            PauseService.Instance.RemoveListener(this);
        }

        private void FixedUpdate()
        {
            if (_isGamePaused) return;

            Move();
        }

        protected virtual void Move()
        {
            transform.position += _setupContext.Direction * (Time.deltaTime * _setupContext.Speed);
        }

        public void OnPause(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }
    }
}