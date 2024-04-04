using UnityEngine;

namespace Dev.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Health _health;
        
        [SerializeField] private PlayerWeaponController _playerWeaponController;

        public Health Health => _health;

        public PlayerWeaponController PlayerWeaponController => _playerWeaponController;
    }
}