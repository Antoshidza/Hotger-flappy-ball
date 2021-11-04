using UnityEngine;
using System;

public class BallController : IBallController
{
    private BallSettings _ballSettings;
    private GameObject _ball;
    private float _ballYVelocity;

    public event Action OnCollide;


    public void Initialize(BallSettings ballSettings, Vector2 position)
    {
        _ballSettings = ballSettings;

        _ball = GameObject.Instantiate(_ballSettings.prefab, position, Quaternion.identity);
        if(!_ball.TryGetComponent<BallCollideDetector>(out var ballCollideDetector))
            throw new Exception($"Instantiated {_ballSettings.prefab.name} must have BallCollideDetector");
        ballCollideDetector.OnCollide += () => { OnCollide?.Invoke(); };
    }

    public void Update()
    {
        UpdateBallVelocity();
        UpdateBallPosition();

        if(Input.GetKeyDown(KeyCode.W))
            Jump();
    }

    private void UpdateBallPosition()
    {
        _ball.transform.Translate(new Vector3(0f, _ballYVelocity, 0f));
    }

    private void UpdateBallVelocity()
    {
        _ballYVelocity = Mathf.Max(_ballYVelocity + -Time.deltaTime * _ballSettings.upAcceleration, -_ballSettings.maxUpVelocity);
    }

    public void Jump()
    {
        _ballYVelocity = _ballSettings.maxUpVelocity;
    }

    public void Clear()
    {
        GameObject.Destroy(_ball);
    }
}
