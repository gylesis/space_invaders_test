using UnityEngine;

namespace Dev.WeaponLogic
{
    [CreateAssetMenu(menuName = "StaticData/Weapons/ShotgunStaticData", fileName = "ShotgunStaticData", order = 0)]
    public class ShotgunStaticData : ProjectileWeaponStaticData
    {
        [SerializeField] private float _bulletsOffset = 0.5f;
            
        [SerializeField] private int _bulletsAmount = 2;

        public float BulletsOffset => _bulletsOffset;

        public int BulletsAmount => _bulletsAmount;
    }
}