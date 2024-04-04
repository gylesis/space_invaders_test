using System.Collections.Generic;
using UnityEngine;

namespace Dev.PlayerLogic
{
    [CreateAssetMenu(menuName = "StaticData/WeaponStaticDataContainer", fileName = "WeaponStaticDataContainer", order = 0)]
    public class WeaponStaticDataContainer : ScriptableObject
    {
        [SerializeField] private List<WeaponStaticData> _staticDatas;

        public List<WeaponStaticData> StaticDatas => _staticDatas;

        public bool TryGetData(WeaponCustomTag tag, out WeaponStaticData weaponStaticData)
        {       
            foreach (WeaponStaticData data in _staticDatas)
            {
                if (data.Tag.AreTagMatch(tag))
                {
                    weaponStaticData = data;
                    return true;
                }
            }

            weaponStaticData = null;
            return false;
        }
    }
}