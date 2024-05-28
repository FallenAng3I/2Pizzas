using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_ESC : MonoBehaviour
{
    public GameObject PauseMenu;

    [Header("Панели для выключения в паузе:")]
    [SerializeField] private GameObject ConstructionMenu;  
    [SerializeField] private GameObject BuildingMenu;      
    [SerializeField] private GameObject UpgradeMenu;       
    [SerializeField] private GameObject MilitaryBaseConstructionMenu;

    private bool isPaused = false;

    void Start()
    {
        PauseMenu.SetActive(false);
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
        PauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void Pause()
    {
        ConstructionMenu.SetActive(false);
        BuildingMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        MilitaryBaseConstructionMenu.SetActive(false);
        
        PauseMenu.SetActive(true);
        
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
