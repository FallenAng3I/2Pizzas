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

    public void StartTimer()
    {
        Debug.Log(gameEndTime);
        Debug.Log(Time.timeScale);
        Debug.Log("start");
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        Debug.Log("yep");
        while (gameEndTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameEndTime -= 1;
            OnTimeChanged?.Invoke(gameEndTime);
            Debug.Log(gameEndTime.ToString());
        }
        OnTimeEnded?.Invoke();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
