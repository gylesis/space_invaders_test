using UnityEngine;

namespace Dev.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private BotConfig _botConfig;

        [SerializeField] private WorldStaticData _worldStaticData;

        public BotConfig BotConfig => _botConfig;
        public WorldStaticData WorldStaticData => _worldStaticData;

        public PlayerConfig PlayerConfig => _playerConfig;
    }
}