using UnityEngine;

namespace Dev.PlayerLogic
{
    public class CustomTagContainer : MonoBehaviour
    {
        [SerializeField] private CustomTag _tag;

        public CustomTag Tag => _tag;
    }
}