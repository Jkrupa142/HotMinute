using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        if (FadeController.Instance != null)
        {
            FadeController.Instance.FadeToScene(sceneName);
        }
        else
        {
         
            SceneManager.LoadScene(sceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
