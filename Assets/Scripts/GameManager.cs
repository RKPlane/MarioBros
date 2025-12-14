using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalEnemies;
    void Awake()
    {
        instance = this;
    }

    public void EnemyKilled()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}

