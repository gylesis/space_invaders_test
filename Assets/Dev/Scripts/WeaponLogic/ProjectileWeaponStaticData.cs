using UnityEngine;

namespace Dev.WeaponLogic
{
    public abstract class ProjectileWeaponStaticData : WeaponStaticData
    {
        [SerializeField] private float _speed = 2;
        [SerializeField] private ProjectileWeaponAmmo _projectileWeaponAmmo;

        public float Speed => _speed;
        public ProjectileWeaponAmmo ProjectileWeaponAmmo => _projectileWeaponAmmo;
    }
}