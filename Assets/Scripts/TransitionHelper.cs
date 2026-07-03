using UnityEngine;

public class TransitionHelper : MonoBehaviour
{
    public void LoadMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMainMenu();
        }
    }
}
