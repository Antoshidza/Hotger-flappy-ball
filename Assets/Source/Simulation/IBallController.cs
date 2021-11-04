using System;
using UnityEngine;

public interface IBallController
{
    event Action OnCollide;

    void Initialize(BallSettings ballSettings, Vector2 position);
    void Jump();
}
