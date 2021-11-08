using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings", order = 1)]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField]
        private GameObject _ballPrefab;
        [SerializeField]
        private GameObject _obstaclePrefab;
        [SerializeField]
        private GameObject _levelWallPrefab;
        [SerializeField]
        private GameDifficulty[] _gameDifficulties;

        public GameDifficulty[] GameDifficulties => _gameDifficulties;

        public GameObject BallPrefab => _ballPrefab;

        public GameObject ObstaclePrefab => _obstaclePrefab;

        public GameObject LevelWallPrefab => _levelWallPrefab;
    }
}