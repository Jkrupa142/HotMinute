using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float totalTime = 60f;
    private float remainingTime;

    [Header("UI Settings")]
    public TextMeshProUGUI timerText;

    private bool isTimeUp = false;

    void Start()
    {
        remainingTime = totalTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isTimeUp)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isTimeUp = true;
                UpdateTimerUI();
            }
            else
            {
                UpdateTimerUI();
            }
        }
    }
    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
        string timeFormatted = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeFormatted;
    }

    public bool IsTimeUp()
    {
        return isTimeUp;
    }
}
