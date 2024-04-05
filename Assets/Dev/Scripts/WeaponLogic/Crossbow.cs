using UnityEngine;

namespace Dev.WeaponLogic
{
    public class Crossbow : ProjectileWeapon
    {
        public override bool TryShoot(Vector2 direction)
        {
            if (AllowToShoot == false)
            {
                return false;
            }

            AllowToShoot = false;

            bool tryGetData = _weaponStaticDataContainer.TryGetData(_weaponTag, out CrossbowGunStaticData staticData);
    
            if (tryGetData == false)
            {   
                Debug.LogError("No data found about this weapon", gameObject);
                return false;
            }

            ProjectileWeaponAmmo projectileWeaponAmmo = Instantiate(staticData.ProjectileWeaponAmmo, ShootPos, Quaternion.Euler(direction));
            
            var setupContext = new ProjectileAmmoSetupContext();
            setupContext.Speed = staticData.Speed;
            setupContext.StartPos = ShootPos;
            setupContext.Direction = direction;
            setupContext.IsOwnerPlayer = _isOwnerPlayer;

            projectileWeaponAmmo.Setup(setupContext);

            RegisterAmmo(projectileWeaponAmmo);

            return true;
        }
    }
}