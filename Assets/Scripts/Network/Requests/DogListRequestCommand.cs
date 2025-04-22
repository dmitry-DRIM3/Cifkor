using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;
using System.Diagnostics;


public class DogListRequestCommand : IRequestCommand
{
    private const string REQUEST_URL = "https://dogapi.dog/api/v2/breeds";

    private readonly Action<List<(string, string)>> onSuccess;
    private UnityWebRequest request;

    public DogListRequestCommand(Action<List<(string, string)>> onSuccess)
    {
        this.onSuccess = onSuccess;
    }

    public async UniTask ExecuteAsync()
    {
        request = UnityWebRequest.Get(REQUEST_URL);
        await request.SendWebRequest();

        var result = JsonConvert.DeserializeObject<DogApiListResponse>(request.downloadHandler.text);
        var dogs = new List<(string, string)>();
        for (int i = 0; i < Math.Min(10, result.data.Length); i++)
        {
            var dog = result.data[i];
            Debug.WriteLine(dog);
            dogs.Add((dog.id, dog.attributes.name));
        }

        onSuccess?.Invoke(dogs);
    }

    public void Cancel()
    {
        request?.Abort();
    }

    [Serializable]
    public class DogApiListResponse
    {
        public DogData[] data;
    }

    [Serializable]
    public class DogData
    {
        public string id;
        public DogAttributes attributes;
    }

    [Serializable]
    public class DogAttributes
    {
        public string name;
    }
}