using UnityEngine;

[CreateAssetMenu(fileName = "GameDifficulty", menuName = "GameDifficulty", order = 1)]
public class GameDifficulty : ScriptableObject
{
    public string Name;
    public Vector2 HeightRange;
    public float ForwardVelocity;
    public BallSettings BallSettings;
    public float ObstacleSpawnFrequency;
    public float VerticalAccelerationIncreaseInterval;
    public float VerticalAccelerationIncreaseStep;
}
