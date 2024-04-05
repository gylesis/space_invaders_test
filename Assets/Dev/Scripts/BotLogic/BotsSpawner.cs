using System.Collections.Generic;
using Dev.StaticData;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Dev.BotLogic
{
    public class BotsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointsParent;
        [SerializeField] private Transform _enemiesParent;
        
        [SerializeField] private BotSpawnPoint _botSpawnPointPrefab;

        [SerializeField] private List<BotSpawnPoint> _botSpawnPoints;

        [SerializeField] private int _rowsCount = 4;
        [SerializeField] private int _columnsCount = 5;

        [SerializeField] private float _pointsOffset = 1.2f;

        private List<Bot> _spawnedBots = new List<Bot>();

        public int WasSpawnedBotsAmount { get; private set; }
        public int DiedBotsAmount => WasSpawnedBotsAmount - _spawnedBots.Count;
            
        private BotFactory _botFactory;
        private GameConfig _gameConfig;

        [ContextMenu(nameof(PrepareSpawnPlaces))]
        private void PrepareSpawnPlaces()
        {
            foreach (var botSpawnPoint in _botSpawnPoints)
            {
                DestroyImmediate(botSpawnPoint.gameObject);
            }
            
            _botSpawnPoints.Clear();
            
            Vector3 spawnPos = transform.position;

            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _columnsCount; j++)
                {
                    BotSpawnPoint botSpawnPoint = Instantiate(_botSpawnPointPrefab, spawnPos ,Quaternion.identity, _spawnPointsParent);

                    botSpawnPoint.transform.SetParent(_spawnPointsParent);
                    _botSpawnPoints.Add(botSpawnPoint);
                    
                    spawnPos += Vector3.left * _pointsOffset;
                }
                
                spawnPos += Vector3.down * _pointsOffset;
                spawnPos.x = transform.position.x;
            }
            
            EditorUtility.SetDirty(this);
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }

        [Inject]
        private void Construct(BotFactory botFactory, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _botFactory = botFactory;
        }
        
        public List<Bot> SpawnEnemies()
        {
            foreach (var botSpawnPoint in _botSpawnPoints)
            {
                BotStaticData staticData = _gameConfig.BotConfig.BotsData[Random.Range(0, _gameConfig.BotConfig.BotsData.Count)];
                
                BotSpawnContext spawnContext = new BotSpawnContext();
                spawnContext.Prefab = staticData.BotPrefab;
                spawnContext.SpawnPos = botSpawnPoint.SpawnPos;
                spawnContext.Parent = _enemiesParent;

                Bot bot = _botFactory.Create(spawnContext);
                _spawnedBots.Add(bot);
            }

            WasSpawnedBotsAmount = _spawnedBots.Count;
            return _spawnedBots;
        }

        public void UnSpawnBot(Bot bot)
        {   
            Destroy(bot.gameObject);
            _spawnedBots.Remove(bot);
        }
        
        private void OnDrawGizmosSelected()
        {
            Vector3 spawnPos = transform.position;

            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _columnsCount; j++)
                {
                    Gizmos.DrawSphere(spawnPos, 0.1f);
                    
                    spawnPos += Vector3.left * _pointsOffset;
                }
                
                spawnPos += Vector3.down * _pointsOffset;
                spawnPos.x = transform.position.x;
            }
        }
    }
}