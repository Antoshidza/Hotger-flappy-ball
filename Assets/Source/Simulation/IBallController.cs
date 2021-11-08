using System;
using UnityEngine;

public interface IBallController
{
    event Action OnCollide;

    void Configure(BallSettings ballSettings);
    void Update(float deltaTime);
    void Jump();
    void Clear();
    void SpawnBall(GameObject ballPrefab);
    void SetBallPosition(Vector2 position);
    void ChangeAcceleration(float value);
    void Reset();
    void Hide();
    void Show();
}
