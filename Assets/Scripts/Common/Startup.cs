using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    private MainPresenter _mainPresenter;

    [Inject]
    public void Construct(MainPresenter mainPresenter)
    {
        _mainPresenter = mainPresenter;
    }

    private void Start()
    {
        _mainPresenter.Initialize();
    }
}
