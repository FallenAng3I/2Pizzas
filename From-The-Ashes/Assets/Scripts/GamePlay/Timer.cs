using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private int timeIntervalsShown = 60;
    [SerializeField] private int rushTime = 120;
    private int lastTime = int.MaxValue;

    private void Start()
    {
        GameEndTimer.OnTimeChanged += ShowTimeIntervals;
    }

    private void ShowTimeIntervals(int time)
    {
        if (time > rushTime)
        {
            if (lastTime - time >= timeIntervalsShown)
            {
                ShowTime(time);
                lastTime = time;
            }
        }
        else
        {
            ShowTime(time);
        }
    }

    private void ShowTime(int time)
    {
        string timeString;
        int minutes = time / 60;
        int seconds = time % 60;
        if (seconds < 10)
        {
            timeString = $"{minutes}:0{seconds}";
        }
        else
        {
            timeString = $"{minutes}:{seconds}";
        }
        
        timerText.text = timeString;
    }
}
