using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class InputProvider : ITickable, ILateTickable
    {
        private InputMap _inputMap;

        public bool ToShoot => _simulatedShootInput || _inputMap.Movement.Shoot.WasPerformedThisFrame();
        public bool ToPause => _inputMap.Movement.Pause.WasPerformedThisFrame();

        public Vector2 MoveVector => _simulatedMoveInput.sqrMagnitude > 0
            ? _simulatedMoveInput
            : _inputMap.Movement.Move.ReadValue<Vector2>();

        private Vector2 _simulatedMoveInput;
        private bool _simulatedShootInput;

        public InputProvider()
        {
            _inputMap = new InputMap();
            _inputMap.Enable();
        }

        public void SimulateShoot()
        {
            _simulatedShootInput = true;
        }
        public void SimulateMoveInput(Vector2 input)
        {
            _simulatedMoveInput = input;
        }

        public void Tick() { }
        public void LateTick()
        {
            _simulatedShootInput = false;
        }
    }
}