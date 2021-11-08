using UnityEngine;
using Zenject;

namespace Core.Presentation
{
    public class LevelWall : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<LevelWall>, ILevelWallFactory
        {
        }

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        private int _speedPropertyID;

        private IGameController _gameController;

        [Inject]
        private void Initialize(IGameController gameController)
        {
            _gameController = gameController;
        }

        private void Start()
        {
            _speedPropertyID = Shader.PropertyToID("_Speed");

            _gameController.OnGameOver += OnGameOver;
            _gameController.OnGameStart += OnGameStart;

            //stretch sprite to level width
            var camera = Camera.main;
            var levelWidth = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - camera.ScreenToWorldPoint(Vector3.zero).x;
            _spriteRenderer.size = new Vector2(levelWidth, _spriteRenderer.size.y);

            StartMove();
        }
        private void OnDestroy()
        {
            _gameController.OnGameOver -= StopMove;
            _gameController.OnGameStart -= StartMove;
        }

        private void SetSpeed(float value)
        {
            _spriteRenderer.material.SetFloat(_speedPropertyID, value / _spriteRenderer.sprite.bounds.size.x);
        }
        private void StartMove()
        {
            SetSpeed(_gameController.ChoosedDifficulty.ForwardVelocity);
        }
        private void StopMove()
        {
            SetSpeed(0f);
        }

        private void OnGameOver()
        {
            StopMove();
            _spriteRenderer.enabled = false;
        }
        private void OnGameStart()
        {
            StartMove();
            _spriteRenderer.enabled = true;
        }
    }
}