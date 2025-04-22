using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;

using UnityEngine.Networking;

public class DogInfoRequestCommand : IRequestCommand
{
    private const string REQUEST_URL = "https://dogapi.dog/api/v2/breeds/";
    private readonly string dogId;
    private readonly Action<string, string> onSuccess;
    private UnityWebRequest request;

    public DogInfoRequestCommand(string dogId, Action<string, string> onSuccess)
    {
        this.dogId = dogId;
        this.onSuccess = onSuccess;
    }

    public async UniTask ExecuteAsync()
    {
        request = UnityWebRequest.Get(REQUEST_URL+dogId);
        await request.SendWebRequest();

        var result = JsonConvert.DeserializeObject<DogResponse>(request.downloadHandler.text);
        var dog = result.data.attributes;
        onSuccess?.Invoke(dog.name, dog.description);
    }

    public void Cancel()
    {
        request?.Abort();
    }

    [Serializable]
    public class DogResponse
    {
        public DogData data;
    }

    [Serializable]
    public class DogData
    {
        public DogAttributes attributes;
    }

    [Serializable]
    public class DogAttributes
    {
        public string name;
        public string description;
    }
}
