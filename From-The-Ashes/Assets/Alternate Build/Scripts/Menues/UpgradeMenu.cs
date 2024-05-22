using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI buildingNameText;

    // Кнопки улучшений и поля стоимости улучшений
    [SerializeField] private Button passiveUpgradeButton;
    [SerializeField] private TextMeshProUGUI passiveUpgradeCostText;
    [SerializeField] private Button clickUpgradeButton;
    [SerializeField] private TextMeshProUGUI clickUpgradeCostText;

    private Building building;

    private void Start()
    {
        BuildingMenu.OnUpgradeButtonClicked += OpenMenu;
        BuildingMenu.OnBuildingMenuClosed += CloseMenu;

        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // Закрываем меню, заполняем поля информации о здании, назначаем здание для кнопок апргредов и устанавливаем цену апгрейдов
    public void OpenMenu(Building newBuilding)
    {
        CloseMenu();

        building = newBuilding;

        buildingNameText.text = building.BuildingInformation.BuildingName;

        passiveUpgradeButton.onClick.AddListener(() => { building.UpgradePassive(); UpdateCost(); });
        clickUpgradeButton.onClick.AddListener(() => { building.UpgradeClick(); UpdateCost(); });
        
        UpdateCost();

        gameObject.SetActive(true);
    }

    // Очищаем информацию о здании и закрываем меню
    public void CloseMenu()
    {
        building = null;

        buildingNameText.text = "";

        passiveUpgradeButton.onClick.RemoveAllListeners();
        passiveUpgradeCostText.text = "";
        clickUpgradeButton.onClick.RemoveAllListeners();
        clickUpgradeCostText.text = "";

        gameObject.SetActive(false);
    }

    private void UpdateCost()
    {
        int clickCostInWood = building.BuildingInformation.CurrentClickUpgradeCostInWood;
        int clickCostInSteel = building.BuildingInformation.CurrentClickUpgradeCostInSteel;
        int clickCostInFuel = building.BuildingInformation.CurrentClickUpgradeCostInFuel;
        int clickCostInLead = building.BuildingInformation.CurrentClickUpgradeCostInLead;

        int passiveCostInWood = building.BuildingInformation.CurrentPassiveUpgradeCostInWood;
        int passiveCostInSteel = building.BuildingInformation.CurrentPassiveUpgradeCostInSteel;
        int passiveCostInFuel = building.BuildingInformation.CurrentPassiveUpgradeCostInFuel;
        int passiveCostInLead = building.BuildingInformation.CurrentPassiveUpgradeCostInLead;

        UpdateCostText(clickCostInWood, clickCostInSteel, clickCostInFuel, clickCostInLead, clickUpgradeCostText);
        UpdateCostText(passiveCostInWood, passiveCostInSteel, passiveCostInFuel, passiveCostInLead, passiveUpgradeCostText);
    }

    public void UpdateCostText(int costInWood, int costInSteel, int costInFuel, int costInLead, TextMeshProUGUI costTextObject)
    {
        string costText = "";

        if (costInWood != 0)
        {
            costText += $"Wood: {costInWood}\r\n";
        }
        if (costInSteel != 0)
        {
            costText += $"Steel: {costInSteel}\r\n";
        }
        if (costInFuel != 0)
        {
            costText += $"Fuel: {costInFuel}\r\n";
        }
        if (costInLead != 0)
        {
            costText += $"Lead: {costInLead}\r\n";
        }

        costTextObject.text = costText;
    }
}
