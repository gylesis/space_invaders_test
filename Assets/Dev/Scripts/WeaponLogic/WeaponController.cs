﻿using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Dev.WeaponLogic
{
    public abstract class WeaponController : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons;

        protected abstract bool IsPlayer { get; }
        
        private Weapon _selectedWeapon;

        public int WeaponCount => _weapons.Count;

        public bool AllowToShoot => _selectedWeapon.AllowToShoot;
        
        private void Start()
        {
            SelectWeapon(0);

            foreach (var weapon in _weapons)
            {
                weapon.AmmoDied.TakeUntilDestroy(this).Subscribe((context => OnAmmoDied(context, weapon)));
                weapon.Setup(IsPlayer);
            }
        }

        protected virtual void OnAmmoDied(AmmoDieContext ammoDieContext, Weapon weapon) { }

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