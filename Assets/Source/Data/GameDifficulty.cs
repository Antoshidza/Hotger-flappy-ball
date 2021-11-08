using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameDifficulty", menuName = "GameDifficulty", order = 1)]
    public class GameDifficulty : ScriptableObject
    {
        [SerializeField]
        public string Name;
        [SerializeField]
        public Vector2 HeightRange;
        [SerializeField]
        public float ForwardVelocity;
        [SerializeField]
        public float ObstacleSpawnFrequency;
        [SerializeField]
        public float VerticalStartVelocity;
        [SerializeField]
        public float VerticalVelocityIncreaseInterval;
        [SerializeField]
        public float VerticalVelocityIncreaseStep;
    }
}