using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [SerializeField] private Button closeButton;

    [Header("Upgrades")]
    [SerializeField] private TextMeshProUGUI clickUpgradeDescriptionText;
    [SerializeField] private ResourcesCountTab clickUpgradeCostTab;
    [SerializeField] private Button clickUpgradeButton;
    [SerializeField] private GameObject passiveUpgradeTab;
    [SerializeField] private TextMeshProUGUI passiveUpgradeDescriptionText;
    [SerializeField] private ResourcesCountTab passiveUpgradeCostTab;
    [SerializeField] private Button passiveUpgradeButton;
    [Space]

    [SerializeField] private UnityEvent upgradeMenuOpenedEvent;
    [SerializeField] private UnityEvent upgradeMenuClosedEvent;
    [SerializeField] private UnityEvent buildingUpgradedEvent;

    private BuildingData buildingData;

    private void Awake()
    {
        passiveUpgradeButton.onClick.AddListener(UpgradePassive);
        clickUpgradeButton.onClick.AddListener(UpgradeClick);
        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // Закрываем меню, заполняем поля информации о здании, назначаем здание для кнопок апргредов и устанавливаем цену апгрейдов
    public void OpenMenu(BuildingData newBuildingInformation)
    {
        CloseMenu();
        
        buildingData = newBuildingInformation;

        if (buildingData.PassiveProductionUpgraded)
            passiveUpgradeTab.SetActive(false);

        clickUpgradeDescriptionText.text = $"+{buildingData.ClickProductionQuantityIncrease} resource / click for:";
        passiveUpgradeDescriptionText.text = $"auto click / {buildingData.PassiveProductionTime} sec for:";

        clickUpgradeCostTab.FillInData(buildingData.ClickUpgradeCost.ToList());
        passiveUpgradeCostTab.FillInData(buildingData.PassiveUpgradeCost.ToList());

        menuWindowObject.SetActive(true);

        upgradeMenuOpenedEvent.Invoke();
    }

    // Очищаем информацию о здании и закрываем меню
    public void CloseMenu()
    {
        buildingData = null;

        passiveUpgradeTab.SetActive(true);

        clickUpgradeDescriptionText.text = "";
        passiveUpgradeDescriptionText.text = "";

        clickUpgradeCostTab.ClearData();
        passiveUpgradeCostTab.ClearData();

        menuWindowObject.SetActive(false);

        upgradeMenuClosedEvent.Invoke();
    }

    // Проверяем, достаточно ли ресурсов, потребляем эти ресурсы и производим апгрейд
    private void UpgradeClick()
    {
        if (buildingData == null) return;

        bool enoughResources = true;

        foreach (ResourceContainer cost in buildingData.ClickUpgradeCost)
        {
            enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
        }

        if (enoughResources)
        {
            foreach (ResourceContainer cost in buildingData.ClickUpgradeCost)
            {
                Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
            }

            buildingData.UpgradeClickProduction();
            buildingUpgradedEvent.Invoke();

            clickUpgradeCostTab.FillInData(buildingData.ClickUpgradeCost.ToList());
        }
    }

    // Проверяем не установлен ли апгрейд уже, если не установлен, то проверяем, достаточно ли ресурсов, потребляем эти ресурсы и производим апгрейд
    private void UpgradePassive()
    {
        if (buildingData == null) return;

        if (!buildingData.PassiveProductionUpgraded)
        {
            bool enoughResources = true;

            foreach (ResourceContainer cost in buildingData.PassiveUpgradeCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (ResourceContainer cost in buildingData.PassiveUpgradeCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }

                buildingData.UpgradePassiveProduction();
                buildingUpgradedEvent.Invoke();

                passiveUpgradeCostTab.FillInData(buildingData.PassiveUpgradeCost.ToList());

                passiveUpgradeTab.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        BuildingMenu.OnUpgradeButtonClicked += OpenMenu;
    }

    private void OnDisable()
    {
        BuildingMenu.OnUpgradeButtonClicked -= OpenMenu;
    }
}
