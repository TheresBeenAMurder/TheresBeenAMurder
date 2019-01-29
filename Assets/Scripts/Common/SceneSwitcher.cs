using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{ 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchScene(int nextScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
