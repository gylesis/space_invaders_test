using System.Collections.Generic;
using UnityEngine;

namespace Dev.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BotConfig", fileName = "BotConfig", order = 0)]
    public class BotConfig : ScriptableObject
    {
        [SerializeField] private List<BotStaticData> _botsData;

        public List<BotStaticData> BotsData => _botsData;
    }
}