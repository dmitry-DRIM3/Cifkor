using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private NavigationView _navigationView;
    [SerializeField] private MainView _mainView;

    public override void InstallBindings()
    {
        
        Container.Bind<NavigationView>().FromInstance(_navigationView).AsSingle();
        Container.Bind<MainView>().FromInstance(_mainView).AsSingle();
        Container.Bind<WeatherView>().FromInstance(_mainView.WeatherView).AsSingle().NonLazy();
        Container.Bind<DogListView>().FromInstance(_mainView.DogListView).AsSingle().NonLazy();


        Container.Bind<WeatherPresenter>().AsSingle();
        Container.Bind<DogListPresenter>().AsSingle();
        Container.Bind<MainPresenter>().AsSingle().NonLazy();
        Container.Bind<RequestQueueService>().AsSingle();
        Container.Bind<DogInfoPopup>().FromComponentInHierarchy().AsSingle();
    }
}