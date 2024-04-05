using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dev.StaticData
{
    [Serializable]
    public class WorldStaticData
    {
        [Range(1f, 20f), Tooltip("Value in %")] [SerializeField]
        private float _borderXPercentOffset = 5;

        [FormerlySerializedAs("_borderYLowerPercentOffset")] [Range(1f, 15f), Tooltip("Value in %")] [SerializeField]
        private float borderYLowerLowerPercentOffset = 5;

        [Range(15f, 25f), Tooltip("Value in %")] [SerializeField]
        private float _borderYUpperPercentOffset = 15;

        public float BorderXPercentOffset => _borderXPercentOffset;
        public float BorderYLowerPercentOffset => borderYLowerLowerPercentOffset;
        public float BorderYUpperPercentOffset => _borderYUpperPercentOffset;
    }
}