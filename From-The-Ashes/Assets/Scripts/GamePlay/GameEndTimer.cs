using System;
using System.Collections;
using UnityEngine;

public class GameEndTimer : MonoBehaviour
{
    [SerializeField] private int gameEndTime = 1200;
    public int GameEndTime { get => gameEndTime; }

    public static event Action<int> OnTimeChanged;
    public static event Action OnTimeEnded;

    // Синглтон, в будущем возможно использование более гибкого ScriptableObject
    public static GameEndTimer Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        OnTimeChanged?.Invoke(gameEndTime);
    }

    private void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (gameEndTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameEndTime -= 1;
            OnTimeChanged?.Invoke(gameEndTime);
        }
        OnTimeEnded?.Invoke();
    }

    private void OnEnable()
    {
        StartWindow.OnGameStarted += StartTimer;
    }

    private void OnDisable()
    {
        StartWindow.OnGameStarted -= StartTimer;
    }
}
