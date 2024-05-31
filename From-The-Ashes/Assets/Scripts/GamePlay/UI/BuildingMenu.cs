using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [SerializeField] private Button closeButton;
    [Space]

    [SerializeField] private Transform defaultTarget;
    [SerializeField] private Transform moveTarget;

    [Header("Building Information")]
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingDescriptionText;
    [SerializeField] private TextMeshProUGUI productionText;
    [SerializeField] private ResourcesCountTab productionInputTab;
    [SerializeField] private ResourcesCountTab productionOutputTab;

    [Header("Building Control Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button demolishButton;
    [Space]

    [SerializeField] private UnityEvent buildingMenuOpenedEvent;
    [SerializeField] private UnityEvent buildingMenuClosedEvent;

    public static event Action<BuildingData> OnUpgradeButtonClicked;

    private Building building = null;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseMenu);
        upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.Invoke(building.BuildingData));
        demolishButton.onClick.AddListener(() => { building.Demolish(); CloseMenu(); });

        CloseMenu();
    }

    public void OpenMenu(Building newBuilding)
    {
        building = newBuilding;

        buildingImage.sprite = building.BuildingData.BuildingIcon;
        buildingNameText.text = building.BuildingData.BuildingName;
        buildingDescriptionText.text = building.BuildingData.BuildingDescription;
        productionInputTab.FillInData(building.BuildingData.ProductionInput);
        productionOutputTab.FillInData(building.BuildingData.ProductionOutput);
        UpdateProductionText();

        menuWindowObject.SetActive(true);

        buildingMenuOpenedEvent.Invoke();
    }

    public void CloseMenu()
    {
        menuWindowObject.SetActive(false);

        building = null;

        buildingImage.sprite = default;
        buildingNameText.text = "";
        buildingDescriptionText.text = "";
        productionText.text = "";
        productionInputTab.ClearData();
        productionOutputTab.ClearData();

        buildingMenuClosedEvent.Invoke();
    }

    public void UpdateProductionText()
    {
        if (building == null) return;

        productionText.text = $"{building.BuildingData.CurrentClickProductionQuantity} cycles / click \r\n";
        if (building.BuildingData.PassiveProductionUpgraded)
        {
            productionText.text += "auto click";
        }
    }

    public void MoveMenu()
    {
        menuWindowObject.transform.position = moveTarget.position;
    }

    public void MoveMenuBack()
    {
        menuWindowObject.transform.position = defaultTarget.position;
    }

    private void OnEnable()
    {
        Building.OnBuildingSelected += OpenMenu;
    }

    private void OnDisable()
    {
        Building.OnBuildingSelected -= OpenMenu;
    }
}
