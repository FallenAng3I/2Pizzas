using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private Building building;

    [SerializeField] private TextMeshProUGUI buildingNameText;

    // ������ ��������� � ���� ��������� ���������
    [SerializeField] private Button passiveUpgradeButton;
    [SerializeField] private TextMeshProUGUI passiveUpgradeCostText;
    [SerializeField] private Button doubleUpgradeButton;
    [SerializeField] private TextMeshProUGUI doubleUpgradeCostText;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // ��������� ����, ��������� ���� ���������� � ������, ��������� ������ ��� ������ ��������� � ������������� ���� ���������
    public void OpenMenu(Building newBuilding)
    {
        CloseMenu();

        building = newBuilding;

        buildingNameText.text = building.buildingName;

        passiveUpgradeButton.onClick.AddListener(() => { building.productionBuilding.UpgradePassive(); UpdateCostText(); });
        doubleUpgradeButton.onClick.AddListener(() => { building.productionBuilding.UpgradeClick(); UpdateCostText(); });
        
        UpdateCostText();

        gameObject.SetActive(true);
    }

    // ������� ���������� � ������ � ��������� ����
    public void CloseMenu()
    {
        building = null;

        buildingNameText.text = "";

        passiveUpgradeButton.onClick.RemoveAllListeners();
        passiveUpgradeCostText.text = "";
        doubleUpgradeButton.onClick.RemoveAllListeners();
        doubleUpgradeCostText.text = "";

        gameObject.SetActive(false);
    }

    private void UpdateCostText()
    {
        passiveUpgradeCostText.text = building.productionBuilding.PassiveUpgradeCostInWood.ToString() + " wood";
        doubleUpgradeCostText.text = building.productionBuilding.DoubleUpgradeCostInWood.ToString() + " wood";
    }
}
