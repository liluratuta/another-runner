using System;
using AnotherRunner.Model.Bonuses.BonusBoxes;
using AnotherRunner.Model.Games;
using AnotherRunner.Model.Levels;
using AnotherRunner.Model.Obstacles;
using AnotherRunner.Model.Players;
using AnotherRunner.Model.Routers;
using AnotherRunner.Model.Simulations;
using AnotherRunner.Model.Spawners;
using AnotherRunner.View;
using UnityEngine;

namespace AnotherRunner.Model.Core
{
    [RequireComponent(typeof(SimulationsUpdater))]
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private LevelInfo _levelInfo;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private ViewFactory _obstacleViewFactory;
        [SerializeField] private ViewFactory _bonusBoxViewFactory;

        private SimulationsUpdater _simulationsUpdater;
        private Game _game;
        private ISpawner<IObstacle> _obstacleSpawner;
        private ISpawner<IBonusBox> _bonusBoxSpawner;
        private PlayerGravitySimulation _playerGravitySimulation;

        private void Awake()
        {
            _simulationsUpdater = GetComponent<SimulationsUpdater>();
            
            Init();
        }

        private void Start()
        {
            _game.Start();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerGravitySimulation.Jump();
            }
        }

        private void OnEnable()
        {
            _obstacleSpawner.Spawned += OnObstacleSpawned;
            _obstacleSpawner.Reclaimed += OnObstacleReclaimed;

            _bonusBoxSpawner.Spawned += OnBonusBoxSpawned;
            _bonusBoxSpawner.Reclaimed += OnBonusBoxReclaimed;
        }

        private void OnDisable()
        {
            _obstacleSpawner.Spawned -= OnObstacleSpawned;
            _obstacleSpawner.Reclaimed -= OnObstacleReclaimed;

            _bonusBoxSpawner.Spawned -= OnBonusBoxSpawned;
            _bonusBoxSpawner.Reclaimed -= OnBonusBoxReclaimed;
        }

        private void OnBonusBoxSpawned(IBonusBox bonusBox) => _bonusBoxViewFactory.Make(bonusBox);
        private void OnBonusBoxReclaimed(IBonusBox bonusBox) => _bonusBoxViewFactory.Reclaim(bonusBox);

        private void OnObstacleSpawned(IObstacle obstacle) => _obstacleViewFactory.Make(obstacle);
        private void OnObstacleReclaimed(IObstacle obstacle) => _obstacleViewFactory.Reclaim(obstacle);

        private void Init()
        {
            var player = new Player(_levelInfo.playerSpawnPoint, Vector2.one, 2f, 1f);

            var runningSimulation = new RunningSimulation(player, _levelInfo);
            _playerGravitySimulation = new PlayerGravitySimulation(player, _levelInfo);
            var timerSimulation = new TimerSimulation();
            var collisionSimulation = new CollisionSimulation(_levelInfo);

            var collisionObserver = new CollisionObserver(player, collisionSimulation);

            _bonusBoxSpawner = new BonusBoxSpawner(runningSimulation, collisionObserver, _levelInfo);
            _obstacleSpawner = new RandomObstacleSpawner(runningSimulation, collisionObserver, _levelInfo);

            _game = new Game(_obstacleSpawner, _bonusBoxSpawner, player, timerSimulation);

            var obstacleEventsRouter =
                new ObstacleEventsRouter(_game, _obstacleSpawner, runningSimulation, collisionObserver);

            var bonusBoxEventsRouter =
                new BonusBoxEventsRouter(_game, _bonusBoxSpawner, runningSimulation, collisionObserver);
            
            collisionSimulation.AddBody(player);
            
            _playerView.Init(player);
            _walletView.Init(player.Wallet);
            _playerStatsView.Init(player);
            _simulationsUpdater.Add(runningSimulation);
            _simulationsUpdater.Add(_playerGravitySimulation);
            _simulationsUpdater.Add(timerSimulation);
            _simulationsUpdater.Add(collisionSimulation);
        }
    }
}