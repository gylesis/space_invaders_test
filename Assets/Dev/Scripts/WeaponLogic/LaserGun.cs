using UnityEngine;

namespace Dev.WeaponLogic
{
    public class LaserGun : ProjectileWeapon
    {
        public override bool TryShoot(Vector2 direction)
        {
            if (AllowToShoot == false)
            {
                return false;
            }

            AllowToShoot = false;

            ProjectileWeaponAmmo projectileWeaponAmmo = Instantiate(_ammoPrefab, ShootPos, Quaternion.Euler(direction));

            bool tryGetData = _weaponStaticDataContainer.TryGetData(_weaponTag, out WeaponStaticData staticData);

            if (tryGetData == false)
            {
                Debug.LogError("No data found about this weapon", gameObject);
                return false;
            }

            var setupContext = new ProjectileAmmoSetupContext();
            setupContext.Speed = _projectileSpeed;
            setupContext.StartPos = ShootPos;
            setupContext.Direction = direction;

            projectileWeaponAmmo.Setup(setupContext);

            RegisterAmmo(projectileWeaponAmmo);

            return true;
        }
    }
}