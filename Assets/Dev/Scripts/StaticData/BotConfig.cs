using System.Collections.Generic;
using Dev.PlayerLogic;
using Dev.Utils;
using UnityEngine;

namespace Dev.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BotConfig", fileName = "BotConfig", order = 0)]
    public class BotConfig : ScriptableObject
    {
        [SerializeField] private List<BotStaticData> _botsData;

        public List<BotStaticData> BotsData => _botsData;

        public bool TryGetData(BotTag tag, out BotStaticData botStaticData)
        {
            foreach (var data in _botsData)
            {
                if (data.Tag.AreTagMatch(tag))
                {
                    botStaticData = data;
                    return true;
                }
            }

            botStaticData = null;
            return false;
        }
    }
}