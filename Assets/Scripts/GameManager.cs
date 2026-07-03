using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public string mainMenuSceneName = "MainMenu";
    
    [Header("Transition Settings")]
    public CameraFollow cameraFollowScript;
    public Animator transitionAnimator;

    private int score = 0;

    void Awake()
    {
        Application.targetFrameRate = 160;
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        if (cameraFollowScript != null)
        {
            cameraFollowScript.ResetToZero();
        }

        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Exit");
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
