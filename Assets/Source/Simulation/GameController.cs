using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameDifficulty _gameDifficulty;
    [SerializeField]
    private Vector3 _ballSpawnPosition;

    private IBallController _ballController;

    [Inject]
    private void Initialize(IBallController ballController)
    {
        _ballController = ballController;
        _ballController.Initialize(_gameDifficulty.ballSettings, _ballSpawnPosition);
    }

    private void Update()
    {
        _ballController.Update();
        if(Input.GetKeyDown(KeyCode.W)) //TODO: subscribe to UI button
            _ballController.Jump();
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        SetGameEnabled(false);
    }

    private void SetGameEnabled(bool value)
    {
        enabled = value;
    }
}
