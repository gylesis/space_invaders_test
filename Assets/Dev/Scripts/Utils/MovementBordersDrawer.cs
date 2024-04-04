using Dev.StaticData;
using UnityEngine;
using Zenject;

namespace Dev.Utils
{
    public class MovementBordersDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        private GameConfig _gameConfig;
        private CameraService _cameraService;

        [Inject]
        private void Construct(CameraService cameraService, GameConfig gameConfig)
        {
            _cameraService = cameraService;
            _gameConfig = gameConfig;
        }

        private void Update()
        {
            if (Time.frameCount % 50 == 0)
            {
                Draw();
            }
        }

        private void Draw()
        {
            float offsetX = (_gameConfig.WorldStaticData.BorderXPercentOffset / 100) * Screen.width;
            float offsetYLower = ((_gameConfig.WorldStaticData.BorderYLowerPercentOffset / 100) * Screen.height);
            float offsetYUpper = ((_gameConfig.WorldStaticData.BorderYUpperPercentOffset / 100) * Screen.height);
                
            Vector2 leftBorder = new Vector2(0 + offsetX, 0 + offsetYUpper);
            Vector2 rightBorder = new Vector2(Screen.width - offsetX, 0 + offsetYLower);

            Vector2 lowerBorder = new Vector2(leftBorder.x, 0 + offsetYLower);
            Vector2 upperBorder = new Vector2(rightBorder.x, 0 + offsetYUpper);
                
            Vector3 rightWorldBorder = _cameraService.Camera.ScreenToWorldPoint(rightBorder);
            Vector3 leftWorldBorder = _cameraService.Camera.ScreenToWorldPoint(leftBorder);
            
            Vector3 lowerWorldBorder = _cameraService.Camera.ScreenToWorldPoint(lowerBorder);
            Vector3 upperWorldBorder = _cameraService.Camera.ScreenToWorldPoint(upperBorder);
    
            float maxX = rightWorldBorder.x;
            float minX = leftWorldBorder.x;

            float maxY = upperWorldBorder.y;
            float minY = lowerWorldBorder.y;

            _lineRenderer.positionCount = 4;
            _lineRenderer.SetPositions(new Vector3[] {lowerWorldBorder, rightWorldBorder, upperWorldBorder, leftWorldBorder});
        }
        
        
    }
}