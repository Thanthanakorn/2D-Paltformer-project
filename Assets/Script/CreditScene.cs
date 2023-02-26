using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    public Button MainMenuButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        MainMenuButton = root.Q<Button>("MainMenu");
        MainMenuButton.clicked += MainMenuButtonPressed;
    }

    void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

}