using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dev.UI
{
    public class MenuService : MonoBehaviour
    {
        [SerializeField] private Transform _menusParent;

        private Dictionary<Type, Menu> _spawnedPrefabs = new Dictionary<Type, Menu>();

        private void Awake()
        {
            AddPopUps(_menusParent.GetComponentsInChildren<Menu>());
        }

        public void AddPopUps(Menu[] menus)
        {
            foreach (Menu menu in menus)
            {
                menu.InitPopUpService(this);
                Type type = menu.GetType();

                _spawnedPrefabs.Add(type, menu);
            }
        }

        public bool TryGetMenu<TMenu>(out TMenu menu) where TMenu : Menu
        {
            menu = null;

            Type menuUpType = typeof(TMenu);

            if (_spawnedPrefabs.TryGetValue(menuUpType, out Menu prefab))
            {
                menu = prefab as TMenu;
                return true;
            }

            _spawnedPrefabs.Add(typeof(TMenu), menu);

            Debug.Log($"No such Menu like {menuUpType}");

            return false;
        }

        public void ShowMenu<TMenu>() where TMenu : Menu
        {
            var tryGetMenu = TryGetMenu<TMenu>(out var menu);

            if (tryGetMenu)
            {
                menu.Show();
            }
        }

        public void HideMenu<TMenu>() where TMenu : Menu
        {
            var tryGetMenu = TryGetMenu<TMenu>(out var menu);

            if (tryGetMenu)
            {
                menu.Hide();
            }
        }

        public void HideAllMenus()
        {
            foreach (var menu in _spawnedPrefabs)
            {
                menu.Value.Hide();
            }
        }
    }
}