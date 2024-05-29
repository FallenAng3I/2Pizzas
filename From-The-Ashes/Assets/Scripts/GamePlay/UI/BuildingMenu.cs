using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] private GameObject buildingMenuWindowObject;
    [SerializeField] private Button closeButton;

    [Header("Building Information")]
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingDescriptionText;

    [Header("Building Control Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button demolishButton;

    private Building building = null;

    [SerializeField] private VoidEvent somethingSelectedEvent;

    public static event Action OnBuildingMenuOpened;
    public static event Action OnBuildingMenuClosed;
    public static event Action<BuildingData> OnUpgradeButtonClicked;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseMenu);
        upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.Invoke(building.BuildingInformation));
        demolishButton.onClick.AddListener(() => { building.Demolish(); CloseMenu(); });

        CloseMenu();
    }

    private void OpenMenu(Building newBuilding)
    {
        building = newBuilding;

        buildingNameText.text = building.BuildingInformation.name;
        buildingDescriptionText.text = building.BuildingInformation.BuildingDescription;

        OnBuildingMenuOpened?.Invoke();

        buildingMenuWindowObject.SetActive(true);
    }

    private void CloseMenu()
    {
        building = null;

        OnBuildingMenuClosed?.Invoke();

        buildingMenuWindowObject.SetActive(false);
    }

    private void OnEnable()
    {
        somethingSelectedEvent.OnEventRaised += CloseMenu;
        Building.OnBuildingSelected += OpenMenu;
        Pause_ESC.OnGamePaused += CloseMenu;
    }

    private void OnDisable()
    {
        somethingSelectedEvent.OnEventRaised -= CloseMenu;
        Building.OnBuildingSelected -= OpenMenu;
        Pause_ESC.OnGamePaused -= CloseMenu;
    }
}
