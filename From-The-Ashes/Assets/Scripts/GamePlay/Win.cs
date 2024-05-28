using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject WinPanelObject;
    [SerializeField] private Button WinButton;

    private void Awake()
    {
        WinPanelObject.SetActive(false);
        MainMission.OnMissionCompleted += ShowWin;
    }

    private void ShowWin()
    {
        Time.timeScale = 0;
        WinButton.onClick.AddListener(Quit);
        WinPanelObject.SetActive(true);
    }

    private void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
