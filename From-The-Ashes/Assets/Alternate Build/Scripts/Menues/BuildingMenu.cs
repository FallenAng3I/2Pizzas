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
    [SerializeField] private Button produceButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button replaceButton;

    public static event Action OnBuildingMenuOpened;
    public static event Action OnBuildingMenuClosed;
    public static event Action<Building> OnUpgradeButtonClicked;
    public static event Action<ConstructionSlot> OnReplaceButtonClicked;

    private void Start()
    {
        Building.OnBuildingSelected += OpenMenu;
        ConstructionSlot.OnConstructionSlotSelected += (slot) => CloseMenu();

        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // ��������� ����, ��������� ���� ���������� � ������, ��������� ��� ������ ��� ������ ���������� � ��������� ����
    private void OpenMenu(Building newBuilding)
    {
        CloseMenu();

        building = newBuilding;

        buildingNameText.text = building.BuildingInformation.BuildingName;
        buildingDescriptionText.text = building.BuildingInformation.BuildingDescription;
        UpdateStopButton();

        produceButton.onClick.AddListener(building.Production.ClickProduction);
        upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.Invoke(building));
        stopButton.onClick.AddListener(TogglePassiveProduction);
        replaceButton.onClick.AddListener(() => OnReplaceButtonClicked?.Invoke(building.GetComponentInParent<ConstructionSlot>()));

        gameObject.SetActive(true);

        OnBuildingMenuOpened?.Invoke();
    }

    // ������� ��� ���������� � ������, ������� �������� � ������, ��������� ������ ����, ��������� �� �������
    public void CloseMenu()
    {
        building = null;

        buildingImage.sprite = default;
        buildingNameText.text = "";
        buildingDescriptionText.text = "";

        produceButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
        stopButton.onClick.RemoveAllListeners();
        replaceButton.onClick.RemoveAllListeners();

        OnBuildingMenuClosed?.Invoke();
 
        gameObject.SetActive(false);
    }

    // ��� ������� �� ������ ��������� ����������� ��������� ������������ � ��������� ������ ���������
    private void TogglePassiveProduction()
    {
        building.Production.TogglePassiveProduction();
        UpdateStopButton();
    }

    // ����� ������ ��������� ���������� � ����������� �� ����, ����������� ������ ��� ���
    private void UpdateStopButton()
    {
        if (building.Production.passiveProductionEnabled)
        {
            stopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            stopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }
    }
}
