using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanelObject;
    [Space] 
    [SerializeField] private UnityEvent gamePausedEvent;

    private bool isPaused = false;

    void Start()
    {
        pausePanelObject.SetActive(false);

        Resume();
    }

    private void Update()
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
        pausePanelObject.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
    
    private void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        pausePanelObject.SetActive(true);

        gamePausedEvent.Invoke();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
