using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Core.Presentation
{
    public class BallUpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _image;

        private IGameController _gameController;
        private IBallController _ballController;

        [Inject]
        private void Initialize(IGameController gameController, IBallController ballController)
        {
            _gameController = gameController;
            _ballController = ballController;
        }

        private void Start()
        {
            _gameController.OnGameStart += OnGameStart;
            _gameController.OnGameOver += OnGameOver;

            gameObject.SetActive(false); //disable visuals by default
        }
        private void OnDestroy()
        {
            _gameController.OnGameStart -= OnGameStart;
            _gameController.OnGameOver -= OnGameOver;
        }

        private void OnGameStart()
        {
            gameObject.SetActive(true);
        }
        private void OnGameOver()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _ballController.SetVerticalDirection(1);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _ballController.SetVerticalDirection(-1);
        }
    }
}