using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuRoot;
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private TextMeshProUGUI _startButtonText;
    [SerializeField]
    private GameObject _resultsPanel;
    [SerializeField]
    private TextMeshProUGUI _scoreAmountText;
    [SerializeField]
    private TextMeshProUGUI _gamesCountText;
    [SerializeField]
    private Dropdown _difficultiesDropdown;

    private IGameController _gameController;
    private IGameSettings _gameSettings;

    [Inject]
    private void Initialize(IGameController gameController, IGameSettings gameSettings)
    {
        _gameController = gameController;
        _gameSettings = gameSettings;
    }

    private void Start()
    {
        _startButton.onClick.AddListener(_gameController.StartGame);

        _gameController.OnGameStart += Close;
        _gameController.OnGameOver += OnGameOver;

        _resultsPanel.SetActive(false); //disable results panel by deault, because we won't show it on 1st game open

        //fill dropdown with difficulties + subscribe to difficulty change
        for(int i = 0; i < _gameSettings.GameDifficulties.Length; i++)
            _difficultiesDropdown.options.Add(new Dropdown.OptionData(_gameSettings.GameDifficulties[i].name));
        _difficultiesDropdown.onValueChanged.AddListener(SetDifficulty);
        _difficultiesDropdown.value = 0; //select 1st difficulty
    }

    private void SetDifficulty(int index)
    {
        _gameController.SetDifficulty(_gameSettings.GameDifficulties[index]);
    }
    private void SetOpen(bool value)
    {
        _menuRoot.SetActive(value);
    }
    private void Close()
    {
        SetOpen(false);
    }
    private void Open()
    {
        SetOpen(true);
    }
    private void OnGameOver()
    {
        Open();

        //show scores
        _resultsPanel.SetActive(true);
        _scoreAmountText.text = _gameController.Score.ToString();
        _gamesCountText.text = _gameController.GamesCount.ToString();

        //change button's text start -> try again
        _startButtonText.text = "try again";
    }
}
