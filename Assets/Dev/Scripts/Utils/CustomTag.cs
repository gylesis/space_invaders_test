using UnityEngine;

namespace Dev.PlayerLogic
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