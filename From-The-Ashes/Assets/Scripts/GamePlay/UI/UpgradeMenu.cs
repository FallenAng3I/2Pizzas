using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [SerializeField] private Button closeButton;

    [Header("Upgrades")]
    [SerializeField] private Button passiveUpgradeButton;
    [SerializeField] private TextMeshProUGUI passiveUpgradeCostText;
    [SerializeField] private Button clickUpgradeButton;
    [SerializeField] private TextMeshProUGUI clickUpgradeCostText;

    private BuildingData buildingInformation;

    private void Start()
    {
        passiveUpgradeButton.onClick.AddListener(UpgradePassive);
        clickUpgradeButton.onClick.AddListener(UpgradeClick);

        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // Закрываем меню, заполняем поля информации о здании, назначаем здание для кнопок апргредов и устанавливаем цену апгрейдов
    private void OpenMenu(BuildingData newBuildingInformation)
    {
        CloseMenu();
        
        buildingInformation = newBuildingInformation;
        UpdateCostText();

        clickUpgradeButton.onClick.AddListener(UpgradeClick);
        passiveUpgradeButton.onClick.AddListener(UpgradePassive);

        menuWindowObject.SetActive(true);
    }

    // Очищаем информацию о здании и закрываем меню
    private void CloseMenu()
    {
        buildingInformation = null;

        passiveUpgradeButton.onClick.RemoveAllListeners();
        passiveUpgradeCostText.text = "";
        clickUpgradeButton.onClick.RemoveAllListeners();
        clickUpgradeCostText.text = "";

        menuWindowObject.SetActive(false);
    }

    // Проверяем, достаточно ли ресурсов, потребляем эти ресурсы и производим апгрейд
    private void UpgradeClick()
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
    private void UpgradePassive()
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

    private void UpdateCostText()
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

    private void OnEnable()
    {
        BuildingMenu.OnUpgradeButtonClicked += OpenMenu;
        BuildingMenu.OnBuildingMenuClosed += CloseMenu;
    }

    private void OnDisable()
    {
        BuildingMenu.OnUpgradeButtonClicked -= OpenMenu;
        BuildingMenu.OnBuildingMenuClosed -= CloseMenu;
    }
}
