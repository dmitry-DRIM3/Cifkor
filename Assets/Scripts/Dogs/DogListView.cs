using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DogListView : MonoBehaviour, IView
{
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject loader;
    [SerializeField] private Transform listContainer;
    [SerializeField] private GameObject dogItemPrefab;

    public event Action<string> OnDogClicked;

    public void Show() => root.SetActive(true);
    public void Hide() => root.SetActive(false);

    public void ShowLoader(bool state) => loader.SetActive(state);

    public void SetDogList(List<(string id, string name)> dogs)
    {
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < dogs.Count; i++)
        {
            var dog = dogs[i];
            var item = Instantiate(dogItemPrefab, listContainer);
            item.GetComponentInChildren<Text>().text = $"{i + 1} - {dog.name}";
            string id = dog.id;
            item.GetComponent<Button>().onClick.AddListener(() => OnDogClicked?.Invoke(id));
        }
    }
}
