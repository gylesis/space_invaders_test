using UniRx;
using UnityEngine;
using Zenject;

namespace Dev.WeaponLogic
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected WeaponCustomTag _weaponTag;

        protected WeaponStaticDataContainer _weaponStaticDataContainer;
        protected bool _isOwnerPlayer;
        public Subject<AmmoDieContext> AmmoDied { get; } = new Subject<AmmoDieContext>();

        public Subject<WeaponAmmo> AmmoSpawned { get; } = new Subject<WeaponAmmo>();
        
        [Inject]
        private void Construct(WeaponStaticDataContainer weaponStaticDataContainer)
        {
            _weaponStaticDataContainer = weaponStaticDataContainer;
        }

        public void Setup(bool isOwnerPlayer)
        {
            _isOwnerPlayer = isOwnerPlayer;
        }

        public Vector3 ShootPos => _shootPoint.position;

        public bool AllowToShoot { get; set; } = true;

        public abstract bool TryShoot(Vector2 direction);
    }
}