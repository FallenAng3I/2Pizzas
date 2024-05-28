using System;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryBaseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button raidsButton;

    public static event Action OnRaidsButtonClicked;

    private void Awake()
    {
        MilitaryBase.OnMilitaryBaseSelected += OpenMenu;
        MilitaryBase.OnMilitaryBaseDeselected += CloseMenu;
        closeButton.onClick.AddListener(CloseMenu);
        raidsButton.onClick.AddListener(() => OnRaidsButtonClicked?.Invoke());

        CloseMenu();
    }

    private void OpenMenu()
    {
        menuWindowObject.SetActive(true);
    }

    private void CloseMenu()
    {
        menuWindowObject.SetActive(false);
    }
}
