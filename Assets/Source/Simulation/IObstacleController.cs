using UnityEngine;

namespace Core
{
    public interface IObstacleController
    {
        void Update(float delta);
        void SetObstacle(GameObject obstaclePrefab);
        void SetHorizontalBounds(float left, float right);
        void Spawn(Vector2 position);
        void Reset();
    }

}