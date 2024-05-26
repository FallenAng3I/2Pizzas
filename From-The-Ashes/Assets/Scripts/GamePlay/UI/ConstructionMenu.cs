using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;

    [Header("Construction Buttons")]
    [SerializeField] private Button sawmillButton;
    [SerializeField] private Button ironMineButton;
    [SerializeField] private Button steelFactoryButton;
    [SerializeField] private Button oilWellButton;
    [SerializeField] private Button fuelFactoryButton;
    [SerializeField] private Button leadMineButton;
    [SerializeField] private Button leadFactoryButton;
    [SerializeField] private Button militaryFactoryButton;

    [Header("Building Prefabs")]
    [SerializeField] private BuildingInformation sawmillPrefab;
    [SerializeField] private BuildingInformation ironMinePrefab;
    [SerializeField] private BuildingInformation steelFactoryPrefab;
    [SerializeField] private BuildingInformation oilWellPrefab;
    [SerializeField] private BuildingInformation fuelFactoryPrefab;
    [SerializeField] private BuildingInformation leadMinePrefab;
    [SerializeField] private BuildingInformation leadFactoryPrefab;
    [SerializeField] private BuildingInformation militaryFactoryPrefab;

    [SerializeField] private ConstructionSlot constructionSlot;

    public static event Action OnBuildingConstructed;

    private void Start()
    {
        // При выборе строительной площадки меню открывается, при выборе здания - закрывается
        ConstructionSlot.OnConstructionSlotSelected += OpenMenu;
        BuildingMenu.OnBuildingMenuOpened += CloseMenu;

        sawmillButton.onClick.AddListener(() => ConstructBuilding(sawmillPrefab, sawmillButton));
        ironMineButton.onClick.AddListener(() => ConstructBuilding(ironMinePrefab, ironMineButton));
        steelFactoryButton.onClick.AddListener(() => ConstructBuilding(steelFactoryPrefab, steelFactoryButton));
        oilWellButton.onClick.AddListener(() => ConstructBuilding(oilWellPrefab, oilWellButton));
        fuelFactoryButton.onClick.AddListener(() => ConstructBuilding(fuelFactoryPrefab, fuelFactoryButton));
        leadMineButton.onClick.AddListener(() => ConstructBuilding(leadMinePrefab, leadMineButton));
        leadFactoryButton.onClick.AddListener(() => ConstructBuilding(leadFactoryPrefab, leadFactoryButton));
        militaryFactoryButton.onClick.AddListener(() => ConstructBuilding(militaryFactoryPrefab, militaryFactoryButton));

        // Назначаем во всплывающие меню информацию о здании для отображения цен
        sawmillButton.GetComponent<PopUpWindow>().buildingInformation = sawmillPrefab;
        ironMineButton.GetComponent<PopUpWindow>().buildingInformation = ironMinePrefab;
        steelFactoryButton.GetComponent<PopUpWindow>().buildingInformation = steelFactoryPrefab;
        oilWellButton.GetComponent<PopUpWindow>().buildingInformation = oilWellPrefab;
        fuelFactoryButton.GetComponent<PopUpWindow>().buildingInformation = fuelFactoryPrefab;
        leadMineButton.GetComponent<PopUpWindow>().buildingInformation = leadMinePrefab;
        leadFactoryButton.GetComponent<PopUpWindow>().buildingInformation = leadFactoryPrefab;
        militaryFactoryButton.GetComponent<PopUpWindow>().buildingInformation = militaryFactoryPrefab;

        CloseMenu();
    }

    private void OpenMenu(ConstructionSlot slot)
    {
        constructionSlot = slot;
        menuWindowObject.SetActive(true);
    }

    private void CloseMenu()
    {
        menuWindowObject.SetActive(false);
        constructionSlot = null;
    }

    // Проверяем, не занят ли слот строительства зданием, проверяем, достаточно ли ресурсов, отнимаем ресурсы, строим здание, увеличиваем цену здания, закрываем меню 
    private void ConstructBuilding(BuildingInformation building, Button constructionButton)
    {
        if (constructionSlot.building == null)
        {
            bool enoughResources = true;

            foreach (Cost cost in building.ConstructionCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (Cost cost in building.ConstructionCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }
                building.IncreaseCurrentConstructionCost();

                GameObject buildingObject = Instantiate(building.BuildingPrefab, constructionSlot.transform);
                OnBuildingConstructed?.Invoke();

                CloseMenu();
            }
        }
    }
}
