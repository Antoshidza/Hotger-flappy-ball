using UnityEngine;
using System;

public class BallController : IBallController
{
    private BallSettings _ballSettings;
    private GameObject _ball;
    private float _verticalVelocity;
    private float _verticalAcceleration;

    public event Action OnCollide;

    public void Configure(BallSettings ballSettings)
    {
        _ballSettings = ballSettings;
    }
    public void SpawnBall(GameObject ballPrefab)
    {
        _ball = GameObject.Instantiate(ballPrefab);
        if(!_ball.TryGetComponent<BallCollideDetector>(out var ballCollideDetector))
            throw new Exception($"Instantiated {ballPrefab.name} must have BallCollideDetector");
        ballCollideDetector.OnCollide += () => { OnCollide?.Invoke(); };
    }

    public void Update(float deltaTime)
    {
        UpdateBallVelocity(deltaTime);
        UpdateBallPosition();
    }

    private void UpdateBallPosition()
    {
        _ball.transform.Translate(new Vector3(0f, _verticalVelocity, 0f));
    }

    private void UpdateBallVelocity(float deltaTime)
    {
        _verticalVelocity = Mathf.Max(_verticalVelocity - deltaTime * _verticalAcceleration, -_ballSettings.maxVerticalVelocity);
    }

    public void Jump()
    {
        _verticalVelocity = _ballSettings.jumpVelocity;
    }

    public void Clear()
    {
        GameObject.Destroy(_ball);
    }

    public void Reset()
    {
        _verticalVelocity = 0f;
        _verticalAcceleration = _ballSettings.verticalStartAcceleration;
    }

    public void SetBallPosition(Vector2 position)
    {
        _ball.transform.position = position;
    }

    public void Hide()
    {
        _ball.SetActive(false);
    }

    public void Show()
    {
        _ball.SetActive(true);
    }

    public void ChangeAcceleration(float value)
    {
        _verticalAcceleration += value;
    }
}
