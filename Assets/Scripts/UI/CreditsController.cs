using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;

    private static string mainMenuScene = "MainMenu";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sceneSwitcher.SwitchScene(mainMenuScene);
        }
    }
}
