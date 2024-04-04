using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class PlayerWeaponController : WeaponController
    {
        private InputProvider _inputProvider;

        [Inject]
        private void Construct(InputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        private void Update()
        {
            if (_inputProvider.ToShoot)
            {
                Shoot(Vector2.up);
            }
        }
    }
}