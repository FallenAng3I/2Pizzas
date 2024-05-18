using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CityScreen");
    }
    
    public void OpenSettings()
    {
        Debug.Log("Открытие настроек...");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}