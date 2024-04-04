using UnityEngine;

namespace Dev.PlayerLogic
{
    [RequireComponent(typeof(CustomTagContainer))]
    public abstract class WeaponAmmo : MonoBehaviour
    {
        [SerializeField] private CustomTagContainer _tagContainer;

        public CustomTagContainer TagContainer => _tagContainer;

        private void Reset()
        {
            _tagContainer = GetComponent<CustomTagContainer>();
        }
    }
}