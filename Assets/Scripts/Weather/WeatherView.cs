using UnityEngine;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour, IView
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Text _weatherText;
    [SerializeField] private Image _weatherIcon;

    public void Show() => _root.SetActive(true);
    public void Hide() => _root.SetActive(false);

    public void SetWeather(string description, int temperatureF)
    {
        _weatherText.text = $"{description} — {temperatureF}F";
    }
}