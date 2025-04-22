using UnityEngine;

public class MainView : MonoBehaviour
{
    [SerializeField] private WeatherView _weatherView;
    [SerializeField] private DogListView _dogListView;

    public WeatherView WeatherView => _weatherView;
    public DogListView DogListView => _dogListView;

    public void ShowWeather()
    {
        _weatherView.Show();
        _dogListView.Hide();
    }

    public void ShowDogs()
    {
        _dogListView.Show();
        _weatherView.Hide();
    }
}
