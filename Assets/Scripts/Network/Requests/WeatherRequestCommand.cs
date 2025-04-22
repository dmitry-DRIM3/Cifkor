using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using UnityEngine;

public class WeatherRequestCommand : IRequestCommand
{
    private const string REQUEST_URL = "https://dog.ceo/api/breeds/list/all";
    private UnityWebRequest _request;
    private readonly Action<string> onSuccess;

    public WeatherRequestCommand(Action<string> onSuccess)
    {
        this.onSuccess = onSuccess;
    }

    public async UniTask ExecuteAsync()
    {
        _request = UnityWebRequest.Get(REQUEST_URL);
        await _request.SendWebRequest();

        if (_request.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(_request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Weather error: {_request.error}");
        }
    }

    public void Cancel()
    {
        if (_request != null && !_request.isDone)
        {
            _request.Abort();
        }
    }
}
