# Hotger flappy ball (accidentally named)
Версия Unity: [2019.4.18f1](https://unity3d.com/ru/get-unity/download/archive)

# Навигация проекта
* [Content](https://github.com/Antoshidza/Hotger-flappy-ball/tree/development/Assets/Content) - содержит графические ассеты, префабы, ScriptableObject'ы и т.д.
* [Source](https://github.com/Antoshidza/Hotger-flappy-ball/tree/development/Assets/Source) - содержит всю кодовую базу

# Simulation
Включает в себя программную часть симуляции игрового процесса:
* [**GameController**](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Simulation/GameController.cs): Верхний уровень симуляции. Наиболее пригодный класс для редактирования. Управляет IBallController. Включает логику управления игрой: подготовка игры/начало игры/конец игры.
* [BallController](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Simulation/BallController.cs): Управляет вертикальным передвижением шара. Предоставляет событие столкновения шара с препятствием.
* [ObstacleController](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Simulation/ObstacleController.cs): Управляет появлением/удалением/передвижением препятствий.
* Прочие вспомогательные классы: [LoopedTimer](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Simulation/LoopedTimer.cs), [GameObjectPool](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Simulation/GameObjectPool.cs), [DIGameControllerInstaller](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Simulation/DIGameControllerInstaller.cs).

# Presentation
Включает в себя программную часть графического представления игровых данных:
* UI
  * [Menu](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Presentation/UI/Menu.cs): Контролирует поведение игрового меню. Переключает сложности игры.
  * [BallUpButton](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Presentation/UI/BallUpButton.cs): Контролирует направление движения шара.
* World
  * [LevelWall](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Presentation/World/LevelWall.cs): Контролирует отображение игровых стен.

# Data (ScriptableObject)
Включает в себя классы для настройки игры и графического отображения:
* [GameDifficulty](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Data/GameDifficulty.cs): Содержит настройки сложности игры (стартовая скорость, интервал увеличения скорости, шаг увелечения скорости, высота стен, частота возникновения препятствий, и т.д.)
* [GameSettings](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Data/GameSettings.cs): Содержит перечисление GameDifficulty доступных для игры и другие настройки, такие как префабы игровых объектов.
* [DISettings](https://github.com/Antoshidza/Hotger-flappy-ball/blob/development/Assets/Source/Data/DISettings.cs): Данные для биндинга Zenject DI.
