using Dev.BotLogic;
using UnityEngine;

namespace Dev.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BotStaticData", fileName = "BotStaticData", order = 0)]
    public class BotStaticData : ScriptableObject
    {
        [SerializeField] private Bot _botPrefab;
        [SerializeField] private int _reward;

        public Bot BotPrefab => _botPrefab;
        public int Reward => _reward;
    }
}