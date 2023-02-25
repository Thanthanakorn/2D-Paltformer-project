using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        
        FindObjectOfType<ScenePersist>().ResetScenePersist();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
            // Before loading next level, have to destroy the ScenePersist object so that
            // the new one of the new level will be there to do the work
            FindObjectOfType<ScenePersist>().ResetScenePersist();
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
