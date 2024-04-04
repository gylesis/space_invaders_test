using UnityEngine;

namespace Dev.PlayerLogic
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        [SerializeField] private WeaponCustomTag _tag;

        public WeaponCustomTag Tag => _tag;
    }
}