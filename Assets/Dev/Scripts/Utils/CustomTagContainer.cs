using UnityEngine;

namespace Dev.Utils
{
    public class CustomTagContainer : MonoBehaviour
    {
        [SerializeField] private CustomTag _tag;

        public CustomTag Tag => _tag;
    }
}