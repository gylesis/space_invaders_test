using UnityEngine;

namespace Dev.PlayerLogic
{
    [RequireComponent(typeof(CustomTagContainer))]
    public abstract class WeaponAmmo : MonoBehaviour
    {
        [SerializeField] private Transform _view;
        
        [SerializeField] private CustomTagContainer _tagContainer;

        public Transform View => _view;

        public CustomTagContainer TagContainer => _tagContainer;

        private void Reset()
        {
            _tagContainer = GetComponent<CustomTagContainer>();
        }
    }
}