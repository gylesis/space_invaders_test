using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected WeaponCustomTag _weaponTag;
       
        protected WeaponStaticDataContainer _weaponStaticDataContainer;

        [Inject]
        private void Construct(WeaponStaticDataContainer weaponStaticDataContainer)
        {
            _weaponStaticDataContainer = weaponStaticDataContainer;
        }
        
        public Vector3 ShootPos => _shootPoint.position;

        public bool AllowToShoot { get; protected set; } = true;
        
        public abstract bool TryShoot(Vector2 direction);
    }
}   