using System.Collections.Generic;
using System.Threading.Tasks;
using Dev.StaticData;
using UniRx;
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

        public List<Bot> SpawnedBots => _spawnedBots;

        public int WasSpawnedBotsAmount { get; private set; }
        public int DiedBotsAmount => WasSpawnedBotsAmount - _spawnedBots.Count;

        private BotFactory _botFactory;
        private GameConfig _gameConfig;

        public Subject<Unit> AllBotsDied { get; } = new Subject<Unit>();

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
                    BotSpawnPoint botSpawnPoint = Instantiate(_botSpawnPointPrefab, spawnPos, Quaternion.identity,
                        _spawnPointsParent);

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

        public async Task SpawnBots()
        {
            foreach (var botSpawnPoint in _botSpawnPoints)
            {
                BotStaticData staticData =
                    _gameConfig.BotConfig.BotsData[Random.Range(0, _gameConfig.BotConfig.BotsData.Count)];

                BotSpawnContext spawnContext = new BotSpawnContext();
                spawnContext.Prefab = staticData.BotPrefab;
                spawnContext.SpawnPos = botSpawnPoint.SpawnPos;
                spawnContext.Parent = _enemiesParent;

                Bot bot = _botFactory.Create(spawnContext);
                bot.ToDie.TakeUntilDestroy(this).Subscribe((reason => OnBotDied(bot, reason)));
                _spawnedBots.Add(bot);

                await Task.Delay(5);
            }

            WasSpawnedBotsAmount = _spawnedBots.Count;
        }

        private void OnBotDied(Bot bot, BotDieReason dieReason)
        {
            UnSpawnBot(bot);

            if (dieReason == BotDieReason.ByRemoving) return;

            if (DiedBotsAmount >= WasSpawnedBotsAmount)
            {
                AllBotsDied.OnNext(Unit.Default);
            }
        }

        public void UnSpawnAllBots()
        {
            for (var index = _spawnedBots.Count - 1; index >= 0; index--)
            {
                var spawnedBot = _spawnedBots[index];
                spawnedBot.ToDie.OnNext(BotDieReason.ByRemoving);
            }
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