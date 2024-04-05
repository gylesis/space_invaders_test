using System;
using UniRx;
using UnityEngine;

namespace Dev.WeaponLogic
{
    public abstract class ProjectileWeapon : Weapon
    {
        protected void RegisterAmmo(ProjectileWeaponAmmo projectileWeaponAmmo)
        {
            projectileWeaponAmmo.ToDie.TakeUntilDestroy(projectileWeaponAmmo).Subscribe(OnAmmoToDie);
        }

        protected void OnAmmoToDie(AmmoDieContext dieContext)
        {
            dieContext.Ammo.View.gameObject.SetActive(false);

            Observable.Timer(TimeSpan.FromTicks(2)).TakeUntilDestroy(this).Subscribe((l => { Destroy(dieContext.Ammo.gameObject); }));

            AmmoDied.OnNext(dieContext);

            AllowToShoot = true;
        }
    }
}