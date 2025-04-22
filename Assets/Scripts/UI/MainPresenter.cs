using System;
using System.Diagnostics;
using Zenject;

public class MainPresenter : IPresenter
{
    private readonly MainView _mainView;
    private readonly NavigationView _navView;
    private readonly WeatherPresenter _weatherPresenter;
    private readonly DogListPresenter _dogPresenter;

    
    public MainPresenter(MainView mainView, NavigationView navView,
                         WeatherPresenter weatherPresenter,
                         DogListPresenter dogPresenter)
    {
        _mainView = mainView;
        _navView = navView;
        _weatherPresenter = weatherPresenter;
        _dogPresenter = dogPresenter;
    }

    public void Initialize()
    {
        _navView.OnWeatherTabClicked += ShowWeather;
        _navView.OnDogsTabClicked += ShowDogs;
        
        ShowWeather(); 
    }

    public void Dispose()
    {
        _navView.OnWeatherTabClicked -= ShowWeather;
        _navView.OnDogsTabClicked -= ShowDogs;
    }

    private void ShowWeather()
    {
        _dogPresenter.Dispose();
        _weatherPresenter.Initialize();
        _mainView.ShowWeather();
    }

    private void ShowDogs()
    {
        _weatherPresenter.Dispose();
        _dogPresenter.Initialize();
        _mainView.ShowDogs();
    }
}
