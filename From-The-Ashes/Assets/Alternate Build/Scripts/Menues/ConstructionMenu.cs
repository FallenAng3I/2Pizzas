using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

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
    [SerializeField] private Building sawmillPrefab;
    [SerializeField] private Building ironMinePrefab;
    [SerializeField] private Building steelFactoryPrefab;
    [SerializeField] private Building oilWellPrefab;
    [SerializeField] private Building fuelFactoryPrefab;
    [SerializeField] private Building leadMinePrefab;
    [SerializeField] private Building leadFactoryPrefab;
    [SerializeField] private Building militaryFactoryPrefab;

    private ConstructionSlot constructionSlot;

    private void Start()
    {
        ConstructionSlot.OnConstructionSlotSelected += OpenMenu;
        BuildingMenu.OnReplaceButtonClicked += OpenMenu;
        BuildingMenu.OnBuildingMenuOpened += CloseMenu;

        sawmillButton.onClick.AddListener(() => ConstructBuilding(sawmillPrefab, sawmillButton));
        ironMineButton.onClick.AddListener(() => ConstructBuilding(ironMinePrefab, ironMineButton));
        steelFactoryButton.onClick.AddListener(() => ConstructBuilding(steelFactoryPrefab, steelFactoryButton));
        oilWellButton.onClick.AddListener(() => ConstructBuilding(oilWellPrefab, oilWellButton));
        fuelFactoryButton.onClick.AddListener(() => ConstructBuilding(fuelFactoryPrefab, fuelFactoryButton));
        leadMineButton.onClick.AddListener(() => ConstructBuilding(leadMinePrefab, leadMineButton));
        leadFactoryButton.onClick.AddListener(() => ConstructBuilding(leadFactoryPrefab, leadFactoryButton));
        militaryFactoryButton.onClick.AddListener(() => ConstructBuilding(militaryFactoryPrefab, militaryFactoryButton));

        SetConstructionButtonBuilingInformation();

        CloseMenu();
    }

    public void OpenMenu(ConstructionSlot slot)
    {
        constructionSlot = slot;
        menuWindow.SetActive(true);
    }

    public void CloseMenu()
    {
        menuWindow.SetActive(false);
        constructionSlot = null;
    }
    
    // Проверяем, не занят ли слот строительства таким же зданием, проверяем, достаточно ли ресурсов, отнимаем ресурсы, строим здание, увеличиваем цену здания, закрываем меню 
    private void ConstructBuilding(Building buildingPrefab, Button constructionButton)
    {
        if (constructionSlot.building == null || constructionSlot.building.BuildingInformation != buildingPrefab.BuildingInformation)
        {
            bool enoughResources = NewResources.WoodNeeded(buildingPrefab.BuildingInformation.CurrentConstructionCostInWood) 
                && NewResources.SteelNeeded(buildingPrefab.BuildingInformation.CurrentConstructionCostInSteel)
                && NewResources.SteelNeeded(buildingPrefab.BuildingInformation.CurrentConstructionCostInFuel)
                && NewResources.SteelNeeded(buildingPrefab.BuildingInformation.CurrentConstructionCostInLead);

            if (enoughResources)
            {
                NewResources.WoodConsumed(buildingPrefab.BuildingInformation.CurrentConstructionCostInWood);
                NewResources.SteelConsumed(buildingPrefab.BuildingInformation.CurrentConstructionCostInSteel);
                NewResources.FuelConsumed(buildingPrefab.BuildingInformation.CurrentConstructionCostInFuel);
                NewResources.LeadConsumed(buildingPrefab.BuildingInformation.CurrentConstructionCostInLead);

                if (constructionSlot.building != null)
                {
                    constructionSlot.building.Demolish();
                }

                Building building = Instantiate(buildingPrefab, constructionSlot.transform);
                constructionSlot.building = building;

                buildingPrefab.BuildingInformation.IncreaseCurrentConstructionCost();

                CloseMenu();
            }
        }
    }

    private void SetConstructionButtonBuilingInformation()
    {
        sawmillButton.GetComponent<PopUpWindow>().buildingInformation = sawmillPrefab.BuildingInformation;
        ironMineButton.GetComponent<PopUpWindow>().buildingInformation = ironMinePrefab.BuildingInformation;
        steelFactoryButton.GetComponent<PopUpWindow>().buildingInformation = steelFactoryPrefab.BuildingInformation;
        oilWellButton.GetComponent<PopUpWindow>().buildingInformation = oilWellPrefab.BuildingInformation;
        fuelFactoryButton.GetComponent<PopUpWindow>().buildingInformation = fuelFactoryPrefab.BuildingInformation;
        leadMineButton.GetComponent<PopUpWindow>().buildingInformation = leadMinePrefab.BuildingInformation;
        leadFactoryButton.GetComponent<PopUpWindow>().buildingInformation = leadFactoryPrefab.BuildingInformation;
        militaryFactoryButton.GetComponent<PopUpWindow>().buildingInformation = militaryFactoryPrefab.BuildingInformation;
    }
}
