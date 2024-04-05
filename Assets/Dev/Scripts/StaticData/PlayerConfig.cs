using UnityEngine;

namespace Dev.StaticData
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "StaticData/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")] [SerializeField] private float _xSpeedModifier = 0.5f;
        [SerializeField] private float _ySpeedModifier = 0.5f;

        public float XSpeedModifier => _xSpeedModifier;
        public float YSpeedModifier => _ySpeedModifier;
    }
}