using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameplaySceneName = "GameplayScene";
    public Animator menuAnimator;
    public Animator BackgroundAnimator;

    void Awake()
    {
        Application.targetFrameRate = 160;
    }

    public void PlayGame()
    {
        if (menuAnimator != null)
        {
            menuAnimator.SetTrigger("Start");
            BackgroundAnimator.SetTrigger("Start");
        }
        else
        {
            LoadGameplayScene();
        }
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
