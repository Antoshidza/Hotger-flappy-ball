using UnityEngine;

namespace Core
{
    public interface IGameSettings
    {
        GameObject BallPrefab { get; }
        GameObject ObstaclePrefab { get; }
        GameObject LevelWallPrefab { get; }
        GameDifficulty[] GameDifficulties { get; }
    }
}