using UnityEngine;

namespace Dev.PlayerLogic
{
    public abstract class ProjectileWeaponStaticData : WeaponStaticData
    {
        [SerializeField] private ProjectileWeaponAmmo _projectileWeaponAmmo;

        public ProjectileWeaponAmmo ProjectileWeaponAmmo => _projectileWeaponAmmo;
    }
}