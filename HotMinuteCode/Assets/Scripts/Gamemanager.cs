using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public string winSceneName = "WinScreen";

    void Start()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
            if (timer == null)
            {
            }
        }
    }

    void Update()
    {
        if (timer != null && timer.IsTimeUp())
        {
            TriggerWinScreen();
        }
    }

    void TriggerWinScreen()
    {
        if (FadeController.Instance != null)
        {
            FadeController.Instance.FadeToScene(winSceneName);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(winSceneName);
        }
    }
}
