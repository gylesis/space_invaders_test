using System;
using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class InputJoystickSimulator : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private InputProvider _inputProvider;

        [Inject]
        private void Construct(InputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        private void Start()
        {
            _joystick.PointerUp += OnJoystickOnPointerUp;
        }

        private void OnDestroy()
        {
            _joystick.PointerUp -= OnJoystickOnPointerUp;
        }

        private void OnJoystickOnPointerUp()
        {
            _inputProvider.SimulateMoveInput(Vector2.zero);
        }

        private void Update()
        {
            Vector2 input = new Vector2(_joystick.Horizontal, _joystick.Vertical);

            if (input.sqrMagnitude == 0) return;

            _inputProvider.SimulateMoveInput(input);
        }
    }
}