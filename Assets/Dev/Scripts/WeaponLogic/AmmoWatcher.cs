using System.Collections.Generic;
using UnityEngine;

namespace Dev.WeaponLogic
{
    public class AmmoWatcher
    {
        private HashSet<ProjectileWeaponAmmo> _ammos = new HashSet<ProjectileWeaponAmmo>();

        public void RegisterAmmo(ProjectileWeaponAmmo weaponAmmo)
        {
            _ammos.Add(weaponAmmo);
        }
        
        public void UnRegisterAmmo(ProjectileWeaponAmmo weaponAmmo)
        {
            _ammos.Remove(weaponAmmo);
        }

        public void DestroyAllAmmos()
        {
            foreach (ProjectileWeaponAmmo weaponAmmo in _ammos)
            {
                Object.Destroy(weaponAmmo.gameObject);
            }
            
            _ammos.Clear();
        }
        
    }
}