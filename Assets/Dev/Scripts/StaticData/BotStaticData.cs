using Dev.BotLogic;
using Dev.PlayerLogic;
using UnityEngine;

namespace Dev.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BotStaticData", fileName = "BotStaticData", order = 0)]
    public class BotStaticData : ScriptableObject
    {
        [SerializeField] private Bot _botPrefab;
        [SerializeField] private int _reward;
        [SerializeField] private BotTag _tag;

        public BotTag Tag => _tag;
        public Bot BotPrefab => _botPrefab;
        public int Reward => _reward;
    }
}