using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives;
    [SerializeField] private int score;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake() 
    {
        // ----------------
        // Singleton pattern
        // ----------------
        // Count how GameSession object at this point
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // If there is a GameSession object already
        if(numGameSessions >= 2)  //  The first one and this one make it two.
        {
            Destroy(gameObject);  // Kill this one so the first one will be the only one.
        } 
        else // This is the first one
        {
            DontDestroyOnLoad(gameObject); // Make this object survive when we load a new scene
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString(); // Make the UI show the initial value
        scoreText.text = score.ToString();       // Make the UI show the initial value
    }

    public void AddToScore(int points) // This is public. When the coin is hit, it will call this.
    {
        score += points;
        scoreText.text = score.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }
    
    public void SetPlayerLives(int newLives)
    {
        playerLives = newLives;
    }


    public void ProcessPlayerDeath() 
    {
        if(playerLives > 1) 
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            livesText.text = playerLives.ToString(); // if we change the value and we want UI to update
                                                     // we must do that manually
        }
        else // Game over.. need to start from the beginning
        {
            // Reset the ScenePersist
            FindObjectOfType<ScenePersist>().ResetScenePersist();

            SceneManager.LoadScene(0); // assume that scene 0 is the first one or the menu
            Destroy(gameObject); // Destroy the game session.


        }
    }
}

