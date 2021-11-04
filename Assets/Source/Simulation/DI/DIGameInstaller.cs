using UnityEngine;
using Zenject;

public class DIGameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _ballPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IBallController>().To<BallController>().FromNew().AsSingle();
    }
}
