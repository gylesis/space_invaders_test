using UnityEngine;

namespace Dev.Utils
{
    [CreateAssetMenu(menuName = "Tags/CustomTag", fileName = "CustomTag", order = 0)]
    public class CustomTag : ScriptableObject
    {
        public bool AreTagMatch(CustomTag customTag)
        {
            return customTag.Equals(this);
        }
    }
}