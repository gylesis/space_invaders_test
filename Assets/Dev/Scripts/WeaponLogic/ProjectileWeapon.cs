using System;
using UniRx;
using UnityEngine;

namespace Dev.WeaponLogic
{
    public abstract class ProjectileWeapon : Weapon
    {
        [SerializeField] protected float _projectileSpeed = 2;
        [SerializeField] protected ProjectileWeaponAmmo _ammoPrefab;

        protected void RegisterAmmo(ProjectileWeaponAmmo projectileWeaponAmmo)
        {
            projectileWeaponAmmo.ToDie.TakeUntilDestroy(projectileWeaponAmmo).Subscribe(OnAmmoToDie);
        }

        protected void OnAmmoToDie(AmmoDieContext dieContext)
        {
            dieContext.Ammo.View.gameObject.SetActive(false);

            Observable.Timer(TimeSpan.FromTicks(2)).Subscribe((l => { Destroy(dieContext.Ammo.gameObject); }));

            AmmoDied.OnNext(dieContext);

            AllowToShoot = true;
        }
    }
}