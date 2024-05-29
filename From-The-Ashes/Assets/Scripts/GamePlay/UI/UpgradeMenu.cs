using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI buildingNameText;

    [Header("Upgrades")]
    [SerializeField] private Button passiveUpgradeButton;
    [SerializeField] private TextMeshProUGUI passiveUpgradeCostText;
    [SerializeField] private Button clickUpgradeButton;
    [SerializeField] private TextMeshProUGUI clickUpgradeCostText;

    private BuildingData buildingInformation;

    private void Awake()
    {
        BuildingMenu.OnUpgradeButtonClicked += OpenMenu;
        BuildingMenu.OnBuildingMenuClosed += CloseMenu;

        passiveUpgradeButton.onClick.AddListener(UpgradePassive);
        clickUpgradeButton.onClick.AddListener(UpgradeClick);

        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // Закрываем меню, заполняем поля информации о здании, назначаем здание для кнопок апргредов и устанавливаем цену апгрейдов
    public void OpenMenu(BuildingData newBuildingInformation)
    {
        Debug.Log("1");
        CloseMenu();
        
        buildingInformation = newBuildingInformation;
        buildingNameText.text = buildingInformation.name;
        UpdateCostText();

        clickUpgradeButton.onClick.AddListener(UpgradeClick);
        passiveUpgradeButton.onClick.AddListener(UpgradePassive);

        menuWindowObject.SetActive(true);
    }

    // Очищаем информацию о здании и закрываем меню
    public void CloseMenu()
    {
        buildingInformation = null;

        buildingNameText.text = "";

        passiveUpgradeButton.onClick.RemoveAllListeners();
        passiveUpgradeCostText.text = "";
        clickUpgradeButton.onClick.RemoveAllListeners();
        clickUpgradeCostText.text = "";

        menuWindowObject.SetActive(false);
    }

    // Проверяем, достаточно ли ресурсов, потребляем эти ресурсы и производим апгрейд
    public void UpgradeClick()
    {
        bool enoughResources = true;

        foreach (Cost cost in buildingInformation.ClickUpgradeCost)
        {
            enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
        }

        if (enoughResources)
        {
            foreach (Cost cost in buildingInformation.ClickUpgradeCost)
            {
                Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
            }

            buildingInformation.UpgradeClickProduction();

            UpdateCostText();
        }
    }

    // Проверяем не установлен ли апгрейд уже, если не установлен, то проверяем, достаточно ли ресурсов, потребляем эти ресурсы и производим апгрейд
    public void UpgradePassive()
    {
        if (!buildingInformation.PassiveProductionUpgraded)
        {
            bool enoughResources = true;

            foreach (Cost cost in buildingInformation.PassiveUpgradeCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (Cost cost in buildingInformation.PassiveUpgradeCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }

                buildingInformation.UpgradePassiveProduction();

                UpdateCostText();
            }
        }
    }

    public void UpdateCostText()
    {
        string costText;

        costText = "";
        foreach (Cost cost in buildingInformation.ClickUpgradeCost)
        {
            costText += $"{cost.Resource.name}: {cost.Quantity}\r\n";
        }
        clickUpgradeCostText.text = costText;

        if (!buildingInformation.PassiveProductionUpgraded)
        {
            costText = "";
            foreach (Cost cost in buildingInformation.PassiveUpgradeCost)
            {
                costText += $"{cost.Resource.name}: {cost.Quantity}\r\n";
            }
            passiveUpgradeCostText.text = costText;
        }
        else
        {
            passiveUpgradeCostText.text = "Upgraded";
        }
    }
}
