using UnityEngine;
using UnityEngine.UI;

public class DogInfoPopup : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;

    public void Show(string name, string description)
    {
        root.SetActive(true);
        nameText.text = name;
        descriptionText.text = description;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
