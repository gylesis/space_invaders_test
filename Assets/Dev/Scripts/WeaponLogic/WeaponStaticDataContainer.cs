using System.Collections.Generic;
using UnityEngine;

namespace Dev.WeaponLogic
{
    [CreateAssetMenu(menuName = "StaticData/WeaponStaticDataContainer", fileName = "WeaponStaticDataContainer",
        order = 0)]
    public class WeaponStaticDataContainer : ScriptableObject
    {
        [SerializeField] private List<WeaponStaticData> _staticDatas;

        public List<WeaponStaticData> StaticDatas => _staticDatas;

        public bool TryGetData<TDataType>(WeaponCustomTag tag, out TDataType weaponStaticData) where TDataType : WeaponStaticData
        {
            foreach (WeaponStaticData data in _staticDatas)
            {
                if (data.Tag.AreTagMatch(tag))
                {
                    weaponStaticData = data as TDataType;
                    return true;
                }
            }

            weaponStaticData = null;
            return false;
        }
    }
}