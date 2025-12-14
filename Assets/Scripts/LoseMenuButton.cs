using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenuButton : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}