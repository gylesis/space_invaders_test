using Dev.PauseLogic;
using Dev.StaticData;
using UnityEngine;
using Zenject;

namespace Dev.PlayerLogic
{
    public class PlayerMovementController : ITickable, IInitializable, IPauseListener
    {
        private Player _player;
        private InputProvider _inputProvider;
        private GameConfig _gameConfig;
        private CameraService _cameraService;

        private bool _isGamePaused;

        public bool IsInputDisabled { get; set; } = true;

        public PlayerMovementController(Player player, InputProvider inputProvider, GameConfig gameConfig,
                                        CameraService cameraService)
        {
            _cameraService = cameraService;
            _gameConfig = gameConfig;
            _inputProvider = inputProvider;
            _player = player;
        }

        public void Initialize()
        {
            PauseService.Instance.RegisterListener(this);
        }

        public void Tick()
        {
            if (_isGamePaused) return;

            if (IsInputDisabled) return;

            float moveX = _inputProvider.MoveVector.x;
            float moveY = _inputProvider.MoveVector.y;

            moveX *= Time.deltaTime * _gameConfig.PlayerConfig.XSpeedModifier;
            moveY *= Time.deltaTime * _gameConfig.PlayerConfig.YSpeedModifier;

            Vector3 move = new Vector3(moveX, moveY, 0);

            _player.transform.position += move;

            ClampPlayerPos();
        }

        public void ClampPlayerPos()
        {
            float offsetX = (_gameConfig.WorldStaticData.BorderXPercentOffset / 100) * Screen.width;
            float offsetYLower = ((_gameConfig.WorldStaticData.BorderYLowerPercentOffset / 100) * Screen.height);
            float offsetYUpper = ((_gameConfig.WorldStaticData.BorderYUpperPercentOffset / 100) * Screen.height);

            Vector2 leftBorder = new Vector2(0 + offsetX, 0);
            Vector2 rightBorder = new Vector2(Screen.width - offsetX, 0);

            Vector2 lowerBorder = new Vector2(0, 0 + offsetYLower);
            Vector2 upperBorder = new Vector2(0, 0 + offsetYUpper);

            Vector3 rightWorldBorder = _cameraService.Camera.ScreenToWorldPoint(rightBorder);
            Vector3 leftWorldBorder = _cameraService.Camera.ScreenToWorldPoint(leftBorder);

            Vector3 lowerWorldBorder = _cameraService.Camera.ScreenToWorldPoint(lowerBorder);
            Vector3 upperWorldBorder = _cameraService.Camera.ScreenToWorldPoint(upperBorder);

            float maxX = rightWorldBorder.x;
            float minX = leftWorldBorder.x;

            float maxY = upperWorldBorder.y;
            float minY = lowerWorldBorder.y;

            Vector3 playerPos = _player.transform.position;

            // X pos
            if (playerPos.x > maxX)
            {
                playerPos.x = maxX;
            }

            if (playerPos.x < minX)
            {
                playerPos.x = minX;
            }

            // Y pos
            if (playerPos.y < minY)
            {
                playerPos.y = minY;
            }

            if (playerPos.y > maxY)
            {
                playerPos.y = maxY;
            }

            // Apply
            _player.transform.position = playerPos;
        }

        public void OnPause(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }
    }
}