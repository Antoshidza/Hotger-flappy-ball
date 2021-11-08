using UnityEngine;
using Zenject;

public class DIGameControllerInstaller : MonoInstaller
{
    [SerializeField]
    private GameController _gameController;

    public override void InstallBindings()
    {
        Container.Bind<IGameController>().To<GameController>().FromInstance(_gameController).AsSingle();
    }
}
