using Dev.PlayerLogic;
using UnityEngine;

namespace Dev.BotLogic
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private BotWeaponController _weaponController;

        public BotWeaponController WeaponController => _weaponController;

        public BotMovementController MovementController;

        private void Awake()
        {
            MovementController = new BotMovementController(this);
        }

        private void Update()
        {
            MovementController.Tick();
        }
    }
}