using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    private Building building = null;

    [SerializeField] private Button closeButton;

    [Header("Building Information")]
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingDescriptionText;

    [Header("Building Control Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button demolishButton;

    public static event Action OnBuildingMenuOpened;
    public static event Action OnBuildingMenuClosed;
    public static event Action<BuildingInformation> OnUpgradeButtonClicked;

    private void Start()
    {
        // При выборе здания меню открывается, при выборе строительной площадки - закрывается
        Building.OnBuildingSelected += OpenMenu;
        ConstructionSlot.OnConstructionSlotSelected += (slot) => CloseMenu();

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

        gameObject.SetActive(true);

        OnBuildingMenuOpened?.Invoke();
    }

    public void CloseMenu()
    {
        building = null;

        buildingImage.sprite = default;
        buildingNameText.text = "";
        buildingDescriptionText.text = "";

        OnBuildingMenuClosed?.Invoke();
 
        gameObject.SetActive(false);
    }
}
