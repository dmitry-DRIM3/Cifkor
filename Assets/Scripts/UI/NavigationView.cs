using UnityEngine;
using UnityEngine.UI;
using System;

public class NavigationView : MonoBehaviour
{
    [SerializeField] private Button _weatherTabButton;
    [SerializeField] private Button _dogsTabButton;

    public event Action OnWeatherTabClicked;
    public event Action OnDogsTabClicked;

    private void Awake()
    {
        _weatherTabButton.onClick.AddListener(() => OnWeatherTabClicked?.Invoke());
        _dogsTabButton.onClick.AddListener(() => OnDogsTabClicked?.Invoke());
    }
}