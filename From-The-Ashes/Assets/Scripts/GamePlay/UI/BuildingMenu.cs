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
    public static event Action<BuildingInformation> OnUpgradeButtonClicked;

    private void Start()
    {
        somethingSelectedEvent.OnEventRaised += CloseMenu;
        Building.OnBuildingSelected += OpenMenu;

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

        buildingMenuWindowObject.SetActive(true);

        OnBuildingMenuOpened?.Invoke();
    }

    private void CloseMenu()
    {
        building = null;

        OnBuildingMenuClosed?.Invoke();

        buildingMenuWindowObject.SetActive(false);
    }
}
