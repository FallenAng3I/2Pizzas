using System;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    [SerializeField] private GameObject startWindowObject;
    [SerializeField] private Button startGameButton;
    public static event Action OnGameStarted;

    private void Awake()
    {
        startWindowObject.SetActive(true);
        startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        startWindowObject.SetActive(false);
        OnGameStarted?.Invoke();
    }
}
