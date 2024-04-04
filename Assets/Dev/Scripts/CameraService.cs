using UnityEngine;

namespace Dev
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public Camera Camera => _camera;
    }
}