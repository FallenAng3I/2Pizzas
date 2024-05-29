using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_ESC : MonoBehaviour
{
    public GameObject PauseMenuObject;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    public static event Action OnGamePaused;
    public static event Action OnSettingsButtonClicked;

    private bool isPaused = false;

    void Start()
    {
        PauseMenuObject.SetActive(false);

        settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        PauseMenuObject.SetActive(false);
        
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void Pause()
    {
        OnGamePaused?.Invoke();
        
        PauseMenuObject.SetActive(true);
        
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
