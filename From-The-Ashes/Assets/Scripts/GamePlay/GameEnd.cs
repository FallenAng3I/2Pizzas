using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    [Header("Win")]
    [SerializeField] private GameObject missionCompletedWindowObject;
    [SerializeField] private Button winButton;
    [SerializeField] private GameObject winPanelObject;

    [Header("Loss")]
    [SerializeField] private GameObject missionFailedWindowObject;
    [SerializeField] private Button lossButton;
    [SerializeField] private GameObject lossPanelObject;

    [Space]
    [SerializeField] private GameObject gameEndPanelObject;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        gameEndPanelObject.SetActive(false);
        missionCompletedWindowObject.SetActive(false);
        missionFailedWindowObject.SetActive(false);
        winPanelObject.SetActive(false);
        lossPanelObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    private void ShowComplition()
    {
        gameEndPanelObject.SetActive(true);
        Time.timeScale = 0;
        winButton.onClick.AddListener(ShowWin);
        missionCompletedWindowObject.SetActive(true);
    }

    private void ShowFailure()
    {
        gameEndPanelObject.SetActive(true);
        Time.timeScale = 0;
        lossButton.onClick.AddListener(ShowLoss);
        missionFailedWindowObject.SetActive(true);
    }

    private void ShowWin()
    {
        missionCompletedWindowObject.SetActive(false);
        quitButton.onClick.AddListener(Quit);
        quitButton.gameObject.SetActive(true);
        winPanelObject.SetActive(true);
    }

    private void ShowLoss()
    {
        missionFailedWindowObject.SetActive(false);
        quitButton.onClick.AddListener(Quit);
        quitButton.gameObject.SetActive(true);
        lossPanelObject.SetActive(true);
    }

    private void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void OnEnable()
    {
        MainMission.OnMissionCompleted += ShowComplition;
        MainMission.OnMissionFailed += ShowFailure;
    }

    private void OnDisable()
    {
        MainMission.OnMissionCompleted -= ShowComplition;
        MainMission.OnMissionFailed -= ShowFailure;
    }
}
