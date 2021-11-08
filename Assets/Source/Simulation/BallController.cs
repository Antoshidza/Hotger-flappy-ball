using UnityEngine;
using System;

namespace Core
{
    public class BallController : IBallController
    {
        private GameObject _ball;
        private float _verticalVelocity;
        private int _verticalDirection = -1;

        public event Action OnCollide;

        public void SpawnBall(GameObject ballPrefab)
        {
#if UNITY_EDITOR
            if(_ball != null)
                throw new Exception($"BallController already have existed {_ball.name}");
#endif
            _ball = GameObject.Instantiate(ballPrefab);
            if(!_ball.TryGetComponent<BallCollideDetector>(out var ballCollideDetector))
                throw new Exception($"Instantiated {ballPrefab.name} must have BallCollideDetector");
            ballCollideDetector.OnCollide += () => { OnCollide?.Invoke(); };
        }
        public void Update(float deltaTime)
        {
            UpdateBallPosition(deltaTime);
        }
        private void UpdateBallPosition(float deltaTime)
        {
            _ball.transform.Translate(new Vector3(0f, _verticalVelocity * deltaTime * _verticalDirection, 0f));
        }
        public void Clear()
        {
            GameObject.Destroy(_ball);
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
        public void SetVerticalDirection(int direction)
        {
            _verticalDirection = direction < 0 ? -1 : 1;
        }
        public void ChangeVerticalVelocity(float value)
        {
            _verticalVelocity += value;
        }
        public void SetVerticalVelocity(float value)
        {
            _verticalVelocity = value;
        }
    }
}