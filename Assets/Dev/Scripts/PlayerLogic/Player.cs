using UnityEngine;

namespace Dev.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponController _playerWeaponController;

        public PlayerWeaponController PlayerWeaponController => _playerWeaponController;
    }
}