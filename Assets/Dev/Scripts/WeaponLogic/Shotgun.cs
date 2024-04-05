using UnityEngine;

namespace Dev.WeaponLogic
{
    public class Shotgun : ProjectileWeapon
    {
        public override bool TryShoot(Vector2 direction)
        {
            if (AllowToShoot == false)
            {
                return false;
            }

            AllowToShoot = false;

            bool tryGetData = _weaponStaticDataContainer.TryGetData(_weaponTag, out ShotgunStaticData staticData);
    
            if (tryGetData == false)
            {
                Debug.LogError("No data found about this weapon", gameObject);
                return false;
            }

            int projectileAmount = staticData.BulletsAmount;

            Vector3 shootPos;

            if (projectileAmount > 1)
            {
                shootPos = ShootPos + Vector3.left *
                    ((Mathf.RoundToInt(((projectileAmount - 1 )/ 2))) * staticData.BulletsOffset);
            }
            else
            {
                shootPos = ShootPos;
            }

            for (int i = 0; i < projectileAmount; i++)
            {
                ProjectileWeaponAmmo projectileWeaponAmmo = Instantiate(staticData.ProjectileWeaponAmmo, shootPos, Quaternion.Euler(direction));

                var setupContext = new ProjectileAmmoSetupContext();
                setupContext.Speed = staticData.Speed;
                setupContext.StartPos = shootPos;
                setupContext.Direction = direction;
                setupContext.IsOwnerPlayer = _isOwnerPlayer;

                projectileWeaponAmmo.Setup(setupContext);

                RegisterAmmo(projectileWeaponAmmo);
                shootPos += Vector3.right * staticData.BulletsOffset;
            }

            return true;
        }
        
    }
    
    
    
}