using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loss : MonoBehaviour
{
    [SerializeField] private GameObject lossPanelObject;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        lossPanelObject.SetActive(false);
        GameEndTimer.OnGameEnded += ShowLoss;
    }

    private void ShowLoss()
    {
        Time.timeScale = 0;
        quitButton.onClick.AddListener(Quit);
        lossPanelObject.SetActive(true);
    }

    private void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
