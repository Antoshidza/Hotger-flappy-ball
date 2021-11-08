using System;
using UnityEngine;

namespace Core
{
    public interface IBallController
    {
        event Action OnCollide;

        void Update(float deltaTime);
        void Clear();
        void SpawnBall(GameObject ballPrefab);
        void SetBallPosition(Vector2 position);
        void SetVerticalDirection(int direction);
        void SetVerticalVelocity(float value);
        void ChangeVerticalVelocity(float value);
        void Hide();
        void Show();
    }
}