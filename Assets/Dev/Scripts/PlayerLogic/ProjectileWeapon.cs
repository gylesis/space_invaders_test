using UniRx;
using UnityEngine;

namespace Dev.PlayerLogic
{
    public abstract class ProjectileWeapon : Weapon
    {
        [SerializeField] protected float _projectileSpeed = 2;
        [SerializeField] protected ProjectileWeaponAmmo _ammoPrefab;
        
        protected void RegisterAmmo(ProjectileWeaponAmmo projectileWeaponAmmo)
        {
            projectileWeaponAmmo.ToDie.TakeUntilDestroy(projectileWeaponAmmo).Subscribe((unit => OnAmmoToDie(projectileWeaponAmmo)));
        }
        
        protected void OnAmmoToDie(ProjectileWeaponAmmo projectileWeaponAmmo)
        {
            Destroy(projectileWeaponAmmo.gameObject);
            
            AllowToShoot = true;
        }
        
        
    }
}