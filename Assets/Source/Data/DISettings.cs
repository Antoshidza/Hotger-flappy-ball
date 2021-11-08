using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DI_Settings", menuName = "DI Settings", order = 1)]
class DISettings : ScriptableObjectInstaller
{
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private GameObject _levelWallPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IBallController>().To<BallController>().FromNew().AsSingle();
        Container.Bind<IObstacleController>().To<ObstacleController>().FromNew().AsSingle();

        Container.Bind<IGameObjectPool>().To<GameObjectPool>().FromNew().AsTransient(); //because every user may want to pool its own gameobject (if need shared pool, then use .WithId(string Identifier))
#if UNITY_EDITOR
        if(_gameSettings == null)
            throw new System.Exception($"NullRef: There is no Game Settings provided for DI Settings!");
#endif
        Container.Bind<IGameSettings>().To<GameSettings>().FromInstance(_gameSettings).AsSingle();

        Container.BindFactory<LevelWall, LevelWall.Factory>().FromComponentInNewPrefab(_levelWallPrefab);
    }
}
