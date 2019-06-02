using System;
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

    public void SwitchScene(string nextScene)
    {
        SceneMap scene = (SceneMap)Enum.Parse(typeof(SceneMap), nextScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
    }

    private enum SceneMap
    {
        MainMenu = 0,
        ChiefBriefing = 1,
        Main = 2,
        GameOver = 3,
        Egg = 4,
        Credits = 5
    }
}
