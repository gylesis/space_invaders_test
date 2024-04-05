using UnityEngine;
using Zenject;

namespace Dev.BotLogic
{
    public class BotIFactory : IFactory<BotSpawnContext, Bot>
    {
        private DiContainer _diContainer;

        public BotIFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public Bot Create(BotSpawnContext param)
        {
            return _diContainer.InstantiatePrefabForComponent<Bot>(param.Prefab.gameObject, param.SpawnPos, Quaternion.identity,param.Parent);
        }
    }

    public class BotFactory : PlaceholderFactory<BotSpawnContext, Bot>
    {
        
    }
    
    public class BotSpawnContext
    {
        public Bot Prefab;
        public Vector3 SpawnPos;
        public Transform Parent;
    }
}