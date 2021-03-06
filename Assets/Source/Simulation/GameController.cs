using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public enum GameState
    {
        Play = 0,
        Pause = 1
    }

    public class GameController : MonoBehaviour, IGameController
    {
        [SerializeField]
        private Vector3 _ballSpawnPosition;
        private GameState _gameState = GameState.Pause;
        private GameDifficulty _choosedDifficulty;
        private float _score;
        private int _gamesCount;
        private GameObject _upWall;
        private GameObject _downWall;

        private IBallController _ballController;
        private IObstacleController _obstacleController;
        private IGameSettings _gameSettings;
        private LoopedTimer _obstacleSpawnTimer;
        private LoopedTimer _verticalVelocityIncreaseTimer;
        private ILevelWallFactory _levelWallsFactory;

        public event Action OnGameStart;
        public event Action OnGameOver;

        public GameDifficulty ChoosedDifficulty => _choosedDifficulty;
        public int Score => (int)_score;
        public int GamesCount => _gamesCount;

        [Inject]
        private void Initialize(IBallController ballController, IObstacleController obstacleController, IGameSettings gameSettings, ILevelWallFactory levelWallsFactory)
        {
            _ballController = ballController;
            _obstacleController = obstacleController;
            _gameSettings = gameSettings;
#if UNITY_EDITOR
            if(_gameSettings.GameDifficulties.Length == 0)
                throw new Exception($"There should be at least 1 difficulty in Game Settings.");
#endif
            SetDifficulty(_gameSettings.GameDifficulties[0]); //choose 1st difficulty by default
            _levelWallsFactory = levelWallsFactory;
        }

        public void StartGame()
        {
            OnGameStart?.Invoke();
            _gameState = GameState.Play;
            _score = 0f;

            //here we want configure stuff only on 1st start
            if(_gamesCount++ == 0)
                ConfigureGame();

            _ballController.SetBallPosition(_ballSpawnPosition);
            _ballController.SetVerticalVelocity(_choosedDifficulty.VerticalStartVelocity);
            _ballController.SetVerticalDirection(-1);
            _ballController.Show();

            SetLevelHeight();
        }
        private void GameOver()
        {
            OnGameOver?.Invoke();
            _gameState = GameState.Pause;

            Reset();
        }
        private void Reset()
        {
            _obstacleController.Reset();
            _ballController.Hide();

            _obstacleSpawnTimer.Reset();
            _verticalVelocityIncreaseTimer.Reset();
        }
        //we want configure game after "Inject" phase because there can be cases when something still not initialized well
        private void ConfigureGame()
        {
            _ballController.SpawnBall(_gameSettings.BallPrefab);
            _ballController.OnCollide += GameOver;

            _obstacleController.SetObstacle(_gameSettings.ObstaclePrefab);
            //set bounds depending on screen width
            var camera = Camera.main;
            _obstacleController.SetHorizontalBounds
            (
                camera.ScreenToWorldPoint(new Vector3(0, 0f, 0f)).x,
                camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x
            );

            //create obstacle timer which will call IObstacleController.Spawn
            _obstacleSpawnTimer = new LoopedTimer(1f / _choosedDifficulty.ObstacleSpawnFrequency);
            _obstacleSpawnTimer.OnExpired += () => { _obstacleController.Spawn(new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0f)).x, UnityEngine.Random.Range(_choosedDifficulty.HeightRange.x, _choosedDifficulty.HeightRange.y))); };

            //create vertical velocity increase timer
            _verticalVelocityIncreaseTimer = new LoopedTimer(_choosedDifficulty.VerticalVelocityIncreaseInterval);
            _verticalVelocityIncreaseTimer.OnExpired += () => { _ballController.ChangeVerticalVelocity(_choosedDifficulty.VerticalVelocityIncreaseStep); };

            //spawn level up/down walls
            _upWall = _levelWallsFactory.Create().gameObject;
            _downWall = _levelWallsFactory.Create().gameObject;
        }
        //we don't want to do anything else then just cache choosed difficulty, because it shouldn't be applyed during running game, but only on GameStart
        public void SetDifficulty(GameDifficulty difficulty)
        {
            _choosedDifficulty = difficulty;
        }
        private void SetLevelHeight()
        {
            _upWall.transform.position = new Vector3(0f, _choosedDifficulty.HeightRange.x, 0f);
            _downWall.transform.position = new Vector3(0f, _choosedDifficulty.HeightRange.y, 0f);
        }

        private void Update()
        {
            if(_gameState == GameState.Pause)
                return;

            _score += Time.deltaTime;

            _ballController.Update(Time.deltaTime);
            _obstacleController.Update(Time.deltaTime * -_choosedDifficulty.ForwardVelocity);
            _obstacleSpawnTimer.Update(Time.deltaTime);

            _verticalVelocityIncreaseTimer.Update(Time.deltaTime);
        }
    }
}