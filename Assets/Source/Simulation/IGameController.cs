using System;

public interface IGameController
{
    event Action OnGameStart;
    event Action OnGameOver;

    GameDifficulty ChoosedDifficulty { get; }
    int Score { get; }
    int GamesCount { get; }

    void StartGame();
    void SetDifficulty(GameDifficulty difficulty);
}
