using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button StartButton;
    public Button CreditButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        StartButton = root.Q<Button>("Start");
        CreditButton = root.Q<Button>("Credit");

        StartButton.clicked += StartButtonPressed;
        CreditButton.clicked += CreditButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("Green-zone");
    }

    void CreditButtonPressed()
    {
        SceneManager.LoadScene("Credit");
    }
}