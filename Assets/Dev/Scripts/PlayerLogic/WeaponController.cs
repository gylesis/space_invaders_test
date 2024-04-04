using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.PlayerLogic
{
    public abstract class WeaponController : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons;

        private Weapon _selectedWeapon;

        public int WeaponCount => _weapons.Count;

        private void Start()
        {
            SelectWeapon(0);
        }

        public void SelectWeapon(int index)
        {
            if (index > _weapons.Count)
            {
                Debug.Log($"Such weapon {index} doesn't exist", gameObject);
                return;
            }

            _selectedWeapon = _weapons[index];
        }
        
        public void Shoot(Vector2 direction)
        {           
            if (_selectedWeapon == null)    
            {
                Debug.LogError($"Weapon not selected", gameObject);
                return;
            }

            _selectedWeapon.TryShoot(direction);
        }
    }
}