using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
