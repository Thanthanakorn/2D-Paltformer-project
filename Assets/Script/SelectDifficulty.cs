using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public  class SelectDifficulty : MonoBehaviour
{
    private Button _easyButton;
    private Button _normalButton;
    private Button _hardButton;
    private GameSession _gameSession;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _easyButton = root.Q<Button>("Easy");
        _normalButton = root.Q<Button>("Normal");
        _hardButton = root.Q<Button>("Hard");
        _gameSession = FindObjectOfType<GameSession>();

        _easyButton.clicked += EasyButtonPressed;
        _normalButton.clicked += NormalButtonPressed;
        _hardButton.clicked += HardButtonPressed;
    }

    void EasyButtonPressed()
    {
        _gameSession.SetPlayerLives(10);
        SceneManager.LoadScene("Green-zone");
    }

    void NormalButtonPressed()
    {
        _gameSession.SetPlayerLives(5);
        SceneManager.LoadScene("Green-zone");
    }
    
    void HardButtonPressed()
    {
        _gameSession.SetPlayerLives(3);
        SceneManager.LoadScene("Green-zone");
    }
}