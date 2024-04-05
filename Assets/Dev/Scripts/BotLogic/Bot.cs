using Dev.PlayerLogic;
using Dev.Utils;
using Dev.WeaponLogic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dev.BotLogic
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private BotWeaponController _weaponController;

        [FormerlySerializedAs("_tag")] [SerializeField]
        private BotTag botTag;

        public BotTag BotTag => botTag;

        public BotWeaponController WeaponController => _weaponController;

        public BotMovementController MovementController;

        public Subject<BotDieReason> ToDie { get; } = new Subject<BotDieReason>();

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